namespace REPS_backend.DTOs.Rutinas
{
    public class RutinaItemDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        
        // Devolvemos el nombre del Enum (ej: "Avanzado") para facilitar la vida al Frontend
        public string Nivel { get; set; } 
        
        public int DuracionMinutos { get; set; }
        public string UrlImagen { get; set; } = string.Empty;
        public string CreadorNombre { get; set; } = string.Empty;
        public int Likes { get; set; }
        
        // ponemos el número total para que sepan si es larga o corta.
        public int CantidadEjercicios { get; set; } 
        public int TotalEjercicios { get; set; }
        
        /// <summary>
        /// ID de la rutina original si es copia de comunidad. Null si es original.
        /// </summary>
        public int? RutinaOrigenId { get; set; }
        
        /// <summary>
        /// Indica si esta rutina fue guardada/copiada de la comunidad
        /// </summary>
        public bool EsCopiaDeComunidad => RutinaOrigenId != null;
        
        /// <summary>
        /// Indica si el usuario actual ya dio like a esta rutina
        /// </summary>
        public bool UsuarioLeDioLike { get; set; } = false;
    }
}
