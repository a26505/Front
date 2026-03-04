using REPS_backend.DTOs.Rutinas;
using REPS_backend.Models;
using REPS_backend.Repositories; // <-- Importamos Repositories

namespace REPS_backend.Services
{
    public class RutinaService : IRutinaService
    {
        private readonly IRutinaRepository _repository;
        private readonly IEjercicioRepository _ejercicioRepository;

        public RutinaService(IRutinaRepository repository, IEjercicioRepository ejercicioRepository)
        {
            _repository = repository;
            _ejercicioRepository = ejercicioRepository;
        }

        public async Task<RutinaDetalleDto> CrearRutinaAsync(RutinaCreateDto dto, int usuarioId)
        {
            // 1. Mapear DTO a Entidad
            var nuevaRutina = new Rutina
            {
                UsuarioId = usuarioId,
                Nombre = dto.Nombre,
                Nivel = dto.Nivel,
                Estado = EstadoRutina.Privada,
                Ejercicios = new List<RutinaEjercicio>()
            };

            // 2. Lógica interna (Smart Weight y Ejercicios)
            int orden = 1;
            foreach (var ejDto in dto.Ejercicios)
            {
                var ejercicioDominio = new RutinaEjercicio
                {
                    EjercicioId = ejDto.EjercicioId,
                    Orden = orden++,
                    Series = ejDto.Series,
                    Repeticiones = ejDto.Repeticiones,
                    DescansoSegundos = ejDto.DescansoSegundos,
                    Tipo = ejDto.Tipo,
                    PorcentajeDelPeso = CalcularPorcentajeSmart(ejDto.Tipo),
                    PesoSugerido = 0
                };
                nuevaRutina.Ejercicios.Add(ejercicioDominio);
            }

            // 3. Calcular duración
            nuevaRutina.DuracionMinutos = CalcularDuracionInterna(nuevaRutina.Ejercicios);

            // 4. USAR EL REPOSITORIO
            await _repository.AddAsync(nuevaRutina); 
            
            // 5. Cargar ejercicios para el mapeo final
            var rutinaCargada = await _repository.GetByIdWithEjerciciosAsync(nuevaRutina.Id);
            return MapToDetalleDto(rutinaCargada ?? nuevaRutina);
        }

        public async Task<List<RutinaItemDto>> ObtenerRutinasPublicasAsync()
        {
            // Pedimos los datos al repositorio real
            var rutinas = await _repository.GetAllPublicasAsync();

            // Convertimos a DTO
            return rutinas.Select(r => new RutinaItemDto
            {
                Id = r.Id,
                Nombre = r.Nombre,
                Nivel = r.Nivel.ToString(),
                DuracionMinutos = r.DuracionMinutos,
                CantidadEjercicios = r.Ejercicios?.Count ?? 0,
                TotalEjercicios = r.Ejercicios?.Count ?? 0,
                CreadorNombre = r.Usuario != null ? r.Usuario.Nombre : "Sistema",
                Likes = r.Likes
            }).ToList();
        }

        public async Task<List<RutinaItemDto>> ObtenerRutinasUsuarioAsync(int usuarioId)
        {
            var rutinas = await _repository.GetByUsuarioIdAsync(usuarioId);

            return rutinas.Select(r => new RutinaItemDto
            {
                Id = r.Id,
                Nombre = r.Nombre,
                Nivel = r.Nivel.ToString(),
                DuracionMinutos = r.DuracionMinutos,
                CantidadEjercicios = r.Ejercicios?.Count ?? 0,
                TotalEjercicios = r.Ejercicios?.Count ?? 0,
                CreadorNombre = "Tú",
                Likes = r.Likes
            }).ToList();
        }

        public async Task<RutinaDetalleDto> ObtenerDetalleRutinaAsync(int rutinaId)
        {
            var rutina = await _repository.GetByIdAsync(rutinaId);

            if (rutina == null) return null;

            return MapToDetalleDto(rutina);
        }

        // --- MÉTODOS PRIVADOS (Los mismos de antes) ---
        private decimal CalcularPorcentajeSmart(TipoSerie tipo)
        {
             return tipo switch
            {
                TipoSerie.Calentamiento => 0.50m,
                TipoSerie.Aproximacion => 0.75m, 
                TipoSerie.DropSet => 0.60m,      
                TipoSerie.AlFallo => 0.85m,      
                _ => 1.0m                        
            };
        }

        private int CalcularDuracionInterna(List<RutinaEjercicio> ejercicios)
        {
            if (ejercicios == null || !ejercicios.Any()) return 0;
            double segundosTotales = 0;
            foreach (var ej in ejercicios)
            {
                segundosTotales += (ej.Series * 60);
                if (ej.Series > 1) segundosTotales += (ej.Series - 1) * ej.DescansoSegundos;
            }
            segundosTotales += (ejercicios.Count * 120);
            return (int)Math.Ceiling(segundosTotales / 60);
        }

        public async Task<RutinaDetalleDto> GenerarRutinaIAAsync(RutinaIARequestDto dto, int usuarioId)
        {
            // 1. Obtener algunos ejercicios reales para no romper FK
            var todosEjercicios = await _ejercicioRepository.GetAllAsync();
            var random = new Random();
            var ejerciciosSeleccionados = todosEjercicios.OrderBy(x => random.Next()).Take(5).ToList();

            if (!ejerciciosSeleccionados.Any())
            {
                // Si no hay ejercicios, devolvemos error o creamos uno temporal (mejor fallar controlado)
                throw new Exception("No hay ejercicios disponibles para generar la rutina.");
            }

            // 1. Crear la Rutina Base
            var rutinaIA = new Rutina
            {
                Nombre = $"IA - {dto.Goal.ToUpper()} {dto.Level.ToUpper()}",
                Nivel = dto.Level.ToLower() == "avanzado" ? NivelDificultad.Avanzado : (dto.Level.ToLower() == "intermedio" ? NivelDificultad.Intermedio : NivelDificultad.Principiante),
                Estado = EstadoRutina.Privada,
                UsuarioId = usuarioId,
                EsGeneradaPorIA = true,
                Ejercicios = new List<RutinaEjercicio>()
            };

            // 2. Generar ejercicios
            int orden = 1;
            foreach (var ej in ejerciciosSeleccionados)
            {
                 var re = new RutinaEjercicio
                 {
                     EjercicioId = ej.Id,
                     Orden = orden++,
                     Series = dto.Goal.ToLower().Contains("fuerza") ? 5 : 3,
                     DescansoSegundos = dto.Goal.ToLower().Contains("fuerza") ? 180 : 90,
                     Tipo = TipoSerie.Normal,
                     PorcentajeDelPeso = 1.0m,
                     PesoSugerido = 0
                 };
                 rutinaIA.Ejercicios.Add(re);
            }

            rutinaIA.DuracionMinutos = CalcularDuracionInterna(rutinaIA.Ejercicios);

            // 3. Guardar en BD
            await _repository.AddAsync(rutinaIA);

            // Recargar para tener los nombres de los ejercicios
            var completa = await _repository.GetByIdWithEjerciciosAsync(rutinaIA.Id);
            return MapToDetalleDto(completa ?? rutinaIA);
        }

        public async Task<int> LikeRutinaAsync(int rutinaId)
        {
            var rutina = await _repository.GetByIdAsync(rutinaId);
            if (rutina == null) return 0;
            
            rutina.Likes++;
            await _repository.UpdateAsync(rutina);
            return rutina.Likes;
        }

        public async Task<RutinaDetalleDto> CopiarRutinaAsync(int rutinaId, int usuarioId)
        {
            var original = await _repository.GetByIdWithEjerciciosAsync(rutinaId);
            if (original == null) throw new Exception("Rutina no encontrada");

            var copia = new Rutina
            {
                Nombre = $"{original.Nombre} (Copia)",
                Nivel = original.Nivel,
                DuracionMinutos = original.DuracionMinutos,
                Estado = EstadoRutina.Privada,
                UsuarioId = usuarioId,
                Ejercicios = original.Ejercicios?.Select(e => new RutinaEjercicio
                {
                    EjercicioId = e.EjercicioId,
                    Series = e.Series,
                    Repeticiones = e.Repeticiones,
                    DescansoSegundos = e.DescansoSegundos,
                    Orden = e.Orden,
                    Tipo = e.Tipo,
                    PorcentajeDelPeso = e.PorcentajeDelPeso,
                    PesoSugerido = e.PesoSugerido
                }).ToList() ?? new List<RutinaEjercicio>()
            };

            await _repository.AddAsync(copia);
            return MapToDetalleDto(copia);
        }

        private RutinaDetalleDto MapToDetalleDto(Rutina r)
        {
            return new RutinaDetalleDto
            {
                Id = r.Id,
                Nombre = r.Nombre,
                Nivel = r.Nivel.ToString(),
                DuracionMinutos = r.DuracionMinutos,
                Estado = r.Estado.ToString(),
                Ejercicios = r.Ejercicios?.Select(e => new RutinaEjercicioDto
                {
                    EjercicioId = e.EjercicioId, 
                    NombreEjercicio = e.Ejercicio?.Nombre ?? "Ejercicio",
                    GrupoMuscular = e.Ejercicio?.GrupoMuscular.ToString().ToUpper() ?? "OTRO",
                    Series = e.Series,
                    DescansoSegundos = e.DescansoSegundos,
                    Tipo = e.Tipo,
                    Repeticiones = e.Repeticiones
                }).ToList() ?? new List<RutinaEjercicioDto>()
            };
        }

        public async Task<bool> EliminarRutinaAsync(int id, int usuarioId)
        {
            var rutina = await _repository.GetByIdAsync(id);
            if (rutina == null) return false;
            
            // Si la aplicación requiere que sólo el creador borre, lo validamos
            if (rutina.UsuarioId != usuarioId) 
                throw new UnauthorizedAccessException("No tienes permisos para eliminar esta rutina.");

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> PublicarRutinaAsync(int id, int usuarioId)
        {
            var rutina = await _repository.GetByIdAsync(id);
            if (rutina == null) return false;
            
            if (rutina.UsuarioId != usuarioId) 
                throw new UnauthorizedAccessException("No tienes permisos para publicar esta rutina.");

            rutina.Estado = EstadoRutina.Publicada;
            await _repository.UpdateAsync(rutina);
            return true;
        }
    }
}
