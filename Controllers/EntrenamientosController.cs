using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using REPS_backend.DTOs.Entrenamientos;
using REPS_backend.Services;
using REPS_backend.Services.AI;
using System.Security.Claims;

namespace REPS_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EntrenamientosController : ControllerBase
    {
        private readonly IEntrenamientoService _entrenamientoService;
        private readonly IRankingService _rankingService;
        private readonly IAIService _aiService;

        public EntrenamientosController(IEntrenamientoService entrenamientoService, IRankingService rankingService, IAIService aiService)
        {
            _entrenamientoService = entrenamientoService;
            _rankingService = rankingService;
            _aiService = aiService;
        }

        [HttpPost("finalizar")]
        public async Task<IActionResult> FinalizarEntrenamiento([FromBody] FinalizarEntrenamientoDto dto)
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return Unauthorized();

            var resultado = await _entrenamientoService.FinalizarEntrenamientoAsync(userId, dto);

            // Recalcular ranking y racha después de cada entrenamiento
            await _rankingService.UpdateUserRankAsync(userId);
            await _rankingService.UpdateStreakAsync(userId);

            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> GetHistorial()
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return Unauthorized();

            var historial = await _entrenamientoService.ObtenerHistorialUsuarioAsync(userId);
            return Ok(historial);
        }

        [HttpGet("tips")]
        public async Task<IActionResult> GetWorkoutTips([FromQuery] string workoutName, [FromQuery] string? muscles)
        {
            var muscleList = string.IsNullOrEmpty(muscles)
                ? new List<string>()
                : muscles.Split(',').Select(m => m.Trim()).ToList();

            var tips = await _aiService.GetWorkoutTipsAsync(workoutName ?? "Entrenamiento", muscleList);
            return Ok(new { tips });
        }

        private int GetCurrentUserId()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int userId))
            {
                return userId;
            }
            return 0;
        }
    }
}
