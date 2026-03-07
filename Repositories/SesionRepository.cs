using Microsoft.EntityFrameworkCore;
using REPS_backend.Data;
using REPS_backend.Models;

namespace REPS_backend.Repositories
{
    public class SesionRepository : ISesionRepository
    {
        private readonly ApplicationDbContext _context;

        public SesionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sesion>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Sesiones
                .Include(s => s.SeriesRealizadas)
                    .ThenInclude(series => series.Ejercicio)
                .Where(s => s.UsuarioId == usuarioId)
                .OrderByDescending(s => s.Fecha)
                .ToListAsync();
        }

        public async Task<Sesion?> GetByIdAsync(int id)
        {
            return await _context.Sesiones
                .Include(s => s.SeriesRealizadas)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Sesion sesion)
        {
            await _context.Sesiones.AddAsync(sesion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sesion sesion)
        {
            _context.Sesiones.Update(sesion);
            await _context.SaveChangesAsync();
        }
    }
}
