using REPS_backend.Models;

namespace REPS_backend.Repositories
{
    public interface IRutinaRepository
    {
        // Obtener todas (para el listado público)
        Task<List<Rutina>> GetAllPublicasAsync(); 
        
        // Obtener una por ID (con sus ejercicios incluidos)
        Task<Rutina?> GetByIdAsync(int id);
        
        Task<Rutina?> GetByIdWithEjerciciosAsync(int id);
        Task<List<Rutina>> GetByUsuarioIdAsync(int usuarioId);

        // Crear
        Task AddAsync(Rutina rutina);
        
        // (Opcional por ahora: Update y Delete)
    }
}