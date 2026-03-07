using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REPS_backend.Data;

namespace REPS_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebugController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DebugController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("rutinas")]
        public async Task<IActionResult> GetRutinas()
        {
            var data = await _context.Rutinas
                .OrderByDescending(r => r.Id)
                .Take(20)
                .Select(r => new 
                {
                    r.Id,
                    r.Nombre,
                    r.ImagenUrl,
                    r.Nivel,
                    r.EsGeneradaPorIA
                })
                .ToListAsync();

            return Ok(data);
        }
    }
}
