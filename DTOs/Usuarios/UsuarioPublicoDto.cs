namespace REPS_backend.DTOs.Usuarios
{
    public class UsuarioPublicoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string AvatarId { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } 


        public string CodigoAmigo { get; set; } = string.Empty;
        public int PuntosTotales { get; set; }
        public int RachaDias { get; set; }
        public string RangoGeneral { get; set; } = string.Empty;
        public bool EsPro { get; set; }
    }
}