using REPS_backend.Models;

namespace REPS_backend.Repositories
{
    public interface ISesionRepository
    {
        Task<List<Sesion>> GetByUsuarioIdAsync(int usuarioId);
        Task<Sesion?> GetByIdAsync(int id);
        Task AddAsync(Sesion sesion);
        Task UpdateAsync(Sesion sesion);
    }
}
