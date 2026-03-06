namespace REPS_backend.DTOs.Rutinas
{
    public class RutinaDetalleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Nivel { get; set; }
        public int DuracionMinutos { get; set; }
        public string UrlImagen { get; set; } = string.Empty;
        public string Estado { get; set; } 
        public int Likes { get; set; }
        public bool IsLikedPorUsuario { get; set; }
        public List<RutinaEjercicioDto> Ejercicios { get; set; }
    }
}