
using Domain.Base.BaseEntity;
using Domain.Entity.Question;

namespace Domain.Entity.Simulacros
{
    public class SimulacroResultEntity : BaseEntity
    {
        public string IdSimulacro { get; set; }
        public string IdEstudiante { get; set; }
        public int Duracion { get; set; }
        public DateTime Fecha { get; set; }
        public int NumCorrectasCiudadanas { get; set; }
        public int NumCorrectasIngles { get; set; }
        public int NumCorrectasRazonamiento { get; set; }
        public int NumCorrectasLectura { get; set; }


        public SimulacroResultEntity()
        {
            this.DateUpdate = DateTime.Now;
        }

        public SimulacroResultEntity(string idSimulacro, string idEstudiante, int duracion, DateTime fecha, int numCorrectasCiudadanas, int numCorrectasIngles, int numCorrectasRazonamiento, int numCorrectasLectura)
        {
            IdSimulacro = idSimulacro;
            IdEstudiante = idEstudiante; 
            Duracion = duracion;
            Fecha = fecha;
            NumCorrectasCiudadanas = numCorrectasCiudadanas;
            NumCorrectasIngles = numCorrectasIngles;
            NumCorrectasRazonamiento = numCorrectasRazonamiento;
            NumCorrectasLectura = numCorrectasLectura;
            this.DateUpdate = DateTime.Now;
        }

    }
}