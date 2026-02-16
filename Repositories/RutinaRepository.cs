using Microsoft.EntityFrameworkCore;
using REPS_backend.Data;
using REPS_backend.Models;

namespace REPS_backend.Repositories
{
    public class RutinaRepository : IRutinaRepository
    {
        private readonly ApplicationDbContext _context;

        public RutinaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Rutina rutina)
        {
            await _context.Rutinas.AddAsync(rutina);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Rutina>> GetAllPublicasAsync()
        {
            // Traemos solo las publicadas e incluimos los ejercicios
            return await _context.Rutinas
                .Where(r => r.Estado == EstadoRutina.Publicada)
                .Include(r => r.Ejercicios)
                .ToListAsync();
        }

        public async Task<Rutina?> GetByIdAsync(int id)
        {
            // Buscamos por ID e incluimos los ejercicios
            return await _context.Rutinas
                .Include(r => r.Ejercicios)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Rutina?> GetByIdWithEjerciciosAsync(int id)
        {
             return await _context.Rutinas
                .Include(r => r.Ejercicios)
                .ThenInclude(re => re.Ejercicio)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Rutina>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Rutinas
                .Where(r => r.UsuarioId == usuarioId)
                .Include(r => r.Ejercicios)
                .ThenInclude(re => re.Ejercicio)
                .ToListAsync();
        }
    }
}