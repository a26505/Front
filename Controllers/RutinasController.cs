using Microsoft.AspNetCore.Mvc;
using REPS_backend.DTOs.Rutinas;
using REPS_backend.Services;

namespace REPS_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class RutinasController : ControllerBase
    {
        private readonly IRutinaService _rutinaService;
        private readonly ILogger<RutinasController> _logger;

        public RutinasController(IRutinaService rutinaService, ILogger<RutinasController> logger)
        {
            _rutinaService = rutinaService;
            _logger = logger;
        }

        private int GetUserId()
        {
            var claim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            return claim != null ? int.Parse(claim.Value) : 0;
        }

        [HttpPost]
        public async Task<ActionResult<RutinaDetalleDto>> CrearRutina([FromBody] RutinaCreateDto dto)
        {
            _logger.LogInformation($"Creando rutina: {dto.Nombre}. Cantidad ejercicios DTO: {dto.Ejercicios?.Count ?? 0}");
            if (dto.Ejercicios != null) {
                foreach(var e in dto.Ejercicios) {
                    _logger.LogInformation($"Ejercicio ID: {e.EjercicioId}, Tipo: {e.Tipo}, Repeticiones: {e.Repeticiones}");
                }
            }
            int usuarioId = GetUserId();
            var rutinaCreada = await _rutinaService.CrearRutinaAsync(dto, usuarioId);
            return CreatedAtAction(nameof(GetRutinaById), new { id = rutinaCreada.Id }, rutinaCreada);
        }

        [HttpGet]
        public async Task<ActionResult<List<RutinaItemDto>>> GetMisRutinas()
        {
            int usuarioId = GetUserId();
            var rutinas = await _rutinaService.ObtenerRutinasUsuarioAsync(usuarioId);
            return Ok(rutinas);
        }

        [HttpGet("comunidad")]
        public async Task<ActionResult<List<RutinaItemDto>>> GetRutinasPublicas()
        {
            var rutinas = await _rutinaService.ObtenerRutinasPublicasAsync();
            return Ok(rutinas);
        }

        // GET: api/rutinas/5
        // (Devuelve el detalle completo "Estilo Netflix Película")
        [HttpGet("{id}")]
        public async Task<ActionResult<RutinaDetalleDto>> GetRutinaById(int id)
        {
            var rutina = await _rutinaService.ObtenerDetalleRutinaAsync(id);

            if (rutina == null)
            {
                return NotFound($"No se encontró ninguna rutina con el ID {id}");
            }

            return Ok(rutina);
        }

        [HttpPost("generar-ia")]
        public async Task<ActionResult<RutinaDetalleDto>> GenerarConIA([FromBody] RutinaIARequestDto dto)
        {
            int usuarioId = GetUserId();
            var rutina = await _rutinaService.GenerarRutinaIAAsync(dto, usuarioId);
            return Ok(rutina);
        }

        [HttpPut("{id}/publicar")]
        public async Task<IActionResult> PublicarRutina(int id)
        {
            try
            {
                int usuarioId = GetUserId();
                var resultado = await _rutinaService.PublicarRutinaAsync(id, usuarioId);
                if (!resultado) return NotFound($"Rutina con ID {id} no encontrada o no se pudo publicar.");
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarRutina(int id)
        {
            try
            {
                int usuarioId = GetUserId();
                var resultado = await _rutinaService.EliminarRutinaAsync(id, usuarioId);
                if (!resultado) return NotFound($"Rutina con ID {id} no encontrada");
                return NoContent();
            }
            catch (UnauthorizedAccessException e)
            {
                return Forbid();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}