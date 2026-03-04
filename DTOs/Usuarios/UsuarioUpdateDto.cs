namespace REPS_backend.DTOs.Usuarios
{
    public class UsuarioUpdateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string AvatarId { get; set; } = string.Empty;
        public string Biografia { get; set; } = string.Empty;
        public bool? EsPerfilPublico { get; set; }
        public bool? MostrarEstadisticas { get; set; }
        public bool? RankingVisible { get; set; }
    }
}