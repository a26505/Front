using REPS_backend.DTOs.Progreso;
using REPS_backend.Models;
using REPS_backend.Repositories;
namespace REPS_backend.Services
{
    public class ProgresoService : IProgresoService
    {
        private readonly IEntrenamientoRepository _entrenamientoRepository;
        private readonly IRecordPersonalRepository _recordRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISesionRepository _sesionRepository;
        private readonly Microsoft.Extensions.Logging.ILogger<ProgresoService> _logger;

        // Constantes de Puntos
        private const int PUNTOS_POR_SERIE = 10;
        private const int PUNTOS_POR_RECORD = 100;

        // Umbrales de Rango (reducidos para pruebas)
        private const int UMBRAL_BRONCE = 30;     // ~3 series
        private const int UMBRAL_PLATA = 100;     // ~10 series
        private const int UMBRAL_ORO = 300;       // ~30 series
        private const int UMBRAL_PLATINO = 600;   // ~60 series
        private const int UMBRAL_DIAMANTE = 1000; // ~100 series
        private const int UMBRAL_LEYENDA = 2000;  // ~200 series

        public ProgresoService(
            IEntrenamientoRepository entrenamientoRepository,
            IRecordPersonalRepository recordRepository,
            IUsuarioRepository usuarioRepository,
            ISesionRepository sesionRepository,
            Microsoft.Extensions.Logging.ILogger<ProgresoService> logger)
        {
            _entrenamientoRepository = entrenamientoRepository;
            _recordRepository = recordRepository;
            _usuarioRepository = usuarioRepository;
            _sesionRepository = sesionRepository;
            _logger = logger;
        }

        public async Task<List<ProgresoMuscularDto>> ObtenerProgresoMuscularAsync(int usuarioId)
        {
            try
            {
                // 1. Obtener todo el historial de ambas fuentes
                var entrenamientos = await _entrenamientoRepository.GetByUsuarioIdWithSeriesAsync(usuarioId);
                var sesiones = await _sesionRepository.GetByUsuarioIdAsync(usuarioId);

                // 2. Agrupar todas las series completadas
                var seriesEntrenos = (entrenamientos ?? new List<Entrenamiento>())
                    .SelectMany(e => e.SeriesRealizadas ?? new List<SerieLog>());

                var seriesSesiones = (sesiones ?? new List<Sesion>())
                    .SelectMany(s => s.SeriesRealizadas ?? new List<SerieLog>());

                var todasLasSeries = seriesEntrenos
                    .Concat(seriesSesiones)
                    .Where(s => s.Completada && s.Ejercicio != null)
                    .ToList();

                // 3. Calcular puntos por grupo
                var puntosPorGrupo = todasLasSeries
                    .GroupBy(s => s.Ejercicio!.GrupoMuscular)
                    .Select(g => new
                    {
                        Grupo = g.Key,
                        Puntos = g.Count() * PUNTOS_POR_SERIE
                    })
                    .ToDictionary(k => k.Grupo, v => v.Puntos);

                // Agregar bonus por records personales
                var records = await _recordRepository.GetByUserIdAsync(usuarioId);
                foreach (var r in records.Where(rec => rec.Ejercicio != null))
                {
                    var grupo = r.Ejercicio!.GrupoMuscular;
                    if (!puntosPorGrupo.ContainsKey(grupo)) puntosPorGrupo[grupo] = 0;
                    puntosPorGrupo[grupo] += PUNTOS_POR_RECORD;
                }

                // 4. Contar actividad por grupo (Entrenamientos + Sesiones)
                var entrenosPorGrupo = new Dictionary<GrupoMuscular, int>();

                // Procesar Entrenamientos
                foreach (var e in entrenamientos ?? new List<Entrenamiento>())
                {
                    if (e.SeriesRealizadas == null) continue;
                    var grupos = e.SeriesRealizadas.Where(s => s.Completada && s.Ejercicio != null).Select(s => s.Ejercicio!.GrupoMuscular).Distinct();
                    foreach (var g in grupos)
                    {
                        if (!entrenosPorGrupo.ContainsKey(g)) entrenosPorGrupo[g] = 0;
                        entrenosPorGrupo[g]++;
                    }
                }

                // Procesar Sesiones
                foreach (var s in sesiones ?? new List<Sesion>())
                {
                    if (s.SeriesRealizadas == null) continue;
                    var grupos = s.SeriesRealizadas.Where(ser => ser.Completada && ser.Ejercicio != null).Select(ser => ser.Ejercicio!.GrupoMuscular).Distinct();
                    foreach (var g in grupos)
                    {
                        if (!entrenosPorGrupo.ContainsKey(g)) entrenosPorGrupo[g] = 0;
                        entrenosPorGrupo[g]++;
                    }
                }

                // 5. Construir DTOs
                var resultado = new List<ProgresoMuscularDto>();
                foreach (GrupoMuscular grupo in Enum.GetValues(typeof(GrupoMuscular)))
                {
                    int puntos = puntosPorGrupo.ContainsKey(grupo) ? puntosPorGrupo[grupo] : 0;
                    int countEntrenos = entrenosPorGrupo.ContainsKey(grupo) ? entrenosPorGrupo[grupo] : 0;

                    var infoRango = CalcularRango(puntos);

                    resultado.Add(new ProgresoMuscularDto
                    {
                        GrupoMuscular = grupo.ToString(),
                        Rango = infoRango.Rango,
                        PuntosActuales = puntos,
                        SiguienteNivelPuntos = infoRango.SiguienteNivel,
                        PuntosParaSiguienteNivel = infoRango.SiguienteNivel - puntos,
                        Porcentaje = infoRango.Porcentaje,
                        EntrenamientosRealizados = countEntrenos
                    });
                }

                return resultado.OrderByDescending(r => r.PuntosActuales).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en ObtenerProgresoMuscularAsync para usuario {UsuarioId}", usuarioId);
                throw;
            }
        }

        public async Task<ProgresoGeneralDto> ObtenerProgresoGeneralAsync(int usuarioId)
        {
            var user = await _usuarioRepository.GetByIdAsync(usuarioId);

            int totalSinLogros = (user?.PuntosTotales ?? 0) - (user?.PuntosLogros ?? 0);

            return new ProgresoGeneralDto
            {
                PuntosTotales = totalSinLogros,
                RangoGeneral = user?.RangoGeneral.ToString() ?? "Bronce"
            };
        }

        private (string Rango, int SiguienteNivel, double Porcentaje) CalcularRango(int puntos)
        {
            if (puntos < UMBRAL_BRONCE)
                return ("Bronce", UMBRAL_BRONCE, (double)puntos / UMBRAL_BRONCE);

            if (puntos < UMBRAL_PLATA)
                return ("Plata", UMBRAL_PLATA, (double)(puntos - UMBRAL_BRONCE) / (UMBRAL_PLATA - UMBRAL_BRONCE));

            if (puntos < UMBRAL_ORO)
                return ("Oro", UMBRAL_ORO, (double)(puntos - UMBRAL_PLATA) / (UMBRAL_ORO - UMBRAL_PLATA));

            if (puntos < UMBRAL_PLATINO)
                return ("Platino", UMBRAL_PLATINO, (double)(puntos - UMBRAL_ORO) / (UMBRAL_PLATINO - UMBRAL_ORO));

            if (puntos < UMBRAL_DIAMANTE)
                return ("Diamante", UMBRAL_DIAMANTE, (double)(puntos - UMBRAL_PLATINO) / (UMBRAL_DIAMANTE - UMBRAL_PLATINO));

            if (puntos < UMBRAL_LEYENDA)
                return ("Leyenda", UMBRAL_LEYENDA, (double)(puntos - UMBRAL_DIAMANTE) / (UMBRAL_LEYENDA - UMBRAL_DIAMANTE));

            return ("Leyenda", puntos, 1.0); // Cap
        }

        public async Task<AnaliticaDto> ObtenerAnaliticaAsync(int usuarioId)
        {
            var entrenamientos = await _entrenamientoRepository.GetByUsuarioIdWithSeriesAsync(usuarioId);
            var sesiones = await _sesionRepository.GetByUsuarioIdAsync(usuarioId);

            // Estructura temporal para unificar series con su fecha respectiva
            var seriesConFecha = new List<(DateTime Fecha, SerieLog Serie)>();

            if (entrenamientos != null)
            {
                foreach (var e in entrenamientos)
                {
                    if (e.SeriesRealizadas != null)
                        seriesConFecha.AddRange(e.SeriesRealizadas.Select(s => (e.Fecha, s)));
                }
            }

            if (sesiones != null)
            {
                foreach (var s in sesiones)
                {
                    if (s.SeriesRealizadas != null)
                        seriesConFecha.AddRange(s.SeriesRealizadas.Select(ser => (s.Fecha, ser)));
                }
            }

            // 1. Métricas Recientes (últimas 7 series completadas)
            // Tomamos el peso, y si el peso es 0 (ej. calistenia), tomamos las repeticiones.
            var pesosList = seriesConFecha
                .Where(x => x.Serie.Completada)
                .OrderBy(x => x.Serie.Id)
                .Select(x => x.Serie.Peso > 0 ? (double)x.Serie.Peso : (double)x.Serie.Repeticiones)
                .TakeLast(7)
                .ToList();

            while (pesosList.Count < 7)
            {
                pesosList.Insert(0, 0.0);
            }

            // 2. Volumen Mensual y Actividad (últimos 6 meses)
            var volumenList = new List<double>();
            var actividad = new List<ActividadMensualDto>();
            var mesesTexto = new[] { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };

            for (int i = 0; i < 6; i++)
            {
                int monthIdx = DateTime.Now.Month - 5 + i;
                int yearOffset = 0;
                if (monthIdx <= 0) { monthIdx += 12; yearOffset = -1; }

                int month = monthIdx;
                int year = DateTime.Now.Year + yearOffset;

                // Contar sesiones únicas en ese mes
                int totalSesionesMes = (entrenamientos?.Count(e => e.Fecha.Year == year && e.Fecha.Month == month) ?? 0)
                                     + (sesiones?.Count(s => s.Fecha.Year == year && s.Fecha.Month == month) ?? 0);

                actividad.Add(new ActividadMensualDto
                {
                    Name = mesesTexto[month - 1],
                    Total = totalSesionesMes,
                    Percent = totalSesionesMes > 0 ? Math.Min(100, (totalSesionesMes * 100) / 20) : 0
                });

                // Calcular volumen total del mes. Si el peso es 0, el multiplicador será 1 (para contar al menos las repeticiones).
                double volumenMes = seriesConFecha
                    .Where(x => x.Fecha.Year == year && x.Fecha.Month == month && x.Serie.Completada)
                    .Sum(x => (double)((x.Serie.Peso > 0 ? x.Serie.Peso : 1) * x.Serie.Repeticiones));

                volumenList.Add(volumenMes);
            }

            return new AnaliticaDto
            {
                Pesos = pesosList,
                Volumen = volumenList,
                ActividadMensual = actividad
            };
        }
    }
}
