using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using REPS_backend.Services;
using System.Security.Claims;

namespace REPS_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProgresoController : ControllerBase
    {
        private readonly IProgresoService _progresoService;

        public ProgresoController(IProgresoService progresoService)
        {
            _progresoService = progresoService;
        }

        [HttpGet("muscular")]
        public async Task<IActionResult> GetProgresoMuscular()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int userId)) return Unauthorized();

            try
            {
                var progreso = await _progresoService.ObtenerProgresoMuscularAsync(userId);
                return Ok(progreso);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("general")]
        public async Task<IActionResult> GetProgresoGeneral()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int userId)) return Unauthorized();

            try
            {
                var general = await _progresoService.ObtenerProgresoGeneralAsync(userId);
                return Ok(general);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("analitica")]
        public async Task<IActionResult> GetAnalitica()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int userId)) return Unauthorized();

            try
            {
                var analitica = await _progresoService.ObtenerAnaliticaAsync(userId);

                // TEMP LOG:
                Console.WriteLine($"[ANALITICA] User: {userId}");
                Console.WriteLine($"[ANALITICA] Pesos count: {analitica.Pesos.Count}, First: {(analitica.Pesos.Any() ? analitica.Pesos.First() : 0)}");
                Console.WriteLine($"[ANALITICA] Volumen count: {analitica.Volumen.Count}, First: {(analitica.Volumen.Any() ? analitica.Volumen.First() : 0)}");
                Console.WriteLine($"[ANALITICA] Actividad: {System.Text.Json.JsonSerializer.Serialize(analitica.ActividadMensual)}");

                return Ok(analitica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
