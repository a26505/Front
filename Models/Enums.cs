namespace REPS_backend.Models
{
    public enum PlanSuscripcion
    {
        Gratuito = 0,
        ProMensual = 1 
    }

    public enum TipoSerie
    {
        Normal = 0,
        Calentamiento = 1, 
        Aproximacion = 2,
        DropSet = 3,       
        AlFallo = 4
    }

    public enum EstadoRutina
    {
        Privada = 0,      
        EnRevision = 1,   
        Publicada = 2,    
        Rechazada = 3     
    }

    public enum NivelDificultad
    {
        Principiante = 0, 
        Intermedio = 1,
        Avanzado = 2      
    }
    
    public enum Rango
    {
        SinRango = 0,
        Bronce = 1,
        Plata = 2,
        Oro = 3,
        Diamante = 4,
        Elite = 5,
        Max = 6
    }
    
    public enum GrupoMuscular
    {
        Pierna = 0,
        Espalda = 1,
        Pecho = 2,
        Hombro = 3,
        Biceps = 4,
        Triceps = 5,
        Abdomen = 6,
        Otro = 7,
        FullBody = 8,
        Cardio = 9
    }
}
