
using Domain.Base.BaseEntity;
using Domain.Entity.QuestionResponseSimulacrum;

namespace Domain.Entity.Simulacros
{
    public class SimulacrumResultEntity : BaseEntity
    {
        public string IdSimulacro { get; set; }
        public string IdEstudiante { get; set; }
        public int Duracion { get; set; }
        public DateTime Fecha { get; set; }
        public int NumCorrectasCiudadanas { get; set; }
        public int NumCorrectasIngles { get; set; }
        public int NumCorrectasRazonamiento { get; set; }
        public int NumCorrectasLectura { get; set; }
        public int TotalCiudadanas { get; set; }
        public int TotalIngles { get; set; }
        public int TotalRazonamiento { get; set; }
        public int TotalLectura { get; set; }
        public double Puntaje { get; set; }
        public List<QuestionResponseSimulacrumEntity> QuestionsResponses { get; set; }
        public string TypeResult { get; set; }


        public SimulacrumResultEntity()
        {
            this.DateUpdate = DateTime.Now;
        }

        public SimulacrumResultEntity(string idSimulacro, string idEstudiante, int duracion, DateTime fecha, int numCorrectasCiudadanas, int numCorrectasIngles, int numCorrectasRazonamiento, int numCorrectasLectura, int totalCiudadanas, int totalIngles, int totalLectura, int totalRazonamiento, double puntaje, List<QuestionResponseSimulacrumEntity> questionsResponses, string type)
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
            TotalCiudadanas = totalCiudadanas;
            TotalIngles = totalIngles;
            TotalLectura = totalLectura;
            TotalRazonamiento = totalRazonamiento;
            Puntaje = puntaje;
            QuestionsResponses = questionsResponses;
            TypeResult = type;
        }

    }
}