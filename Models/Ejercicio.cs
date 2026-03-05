namespace REPS_backend.Models
{
    public class Ejercicio
    {
        public int Id { get; set; }
        public int? UsuarioCreadorId { get; set; } 

        public string Nombre { get; set; } = ""; 
        
        public string DescripcionTecnica { get; set; } = ""; 
        
        public string ImagenMusculosUrl { get; set; } = ""; 
        public string? VideoUrl { get; set; }
        public GrupoMuscular GrupoMuscular { get; set; } = GrupoMuscular.Otro;
        public List<DetalleMuscular> MusculosInvolucrados { get; set; } = new List<DetalleMuscular>();
        
        /// <summary>
        /// Indica si el ejercicio es unilateral (se trabaja un lado a la vez)
        /// </summary>
        public bool EsUnilateral { get; set; } = false;
        
        /// <summary>
        /// Indica si el ejercicio es de cardio (usa minutos en vez de series/reps)
        /// </summary>
        public bool EsCardio { get; set; } = false;
    }
}
