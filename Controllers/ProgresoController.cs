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

            var progreso = await _progresoService.ObtenerProgresoMuscularAsync(userId);
            return Ok(progreso);
        }

        [HttpGet("general")]
        public async Task<IActionResult> GetProgresoGeneral()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int userId)) return Unauthorized();

            var general = await _progresoService.ObtenerProgresoGeneralAsync(userId);
            return Ok(general);
        }

        [HttpGet("analitica")]
        public async Task<IActionResult> GetAnalitica()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int userId)) return Unauthorized();

            var analitica = await _progresoService.ObtenerAnaliticaAsync(userId);
            return Ok(analitica);
        }
    }
}
