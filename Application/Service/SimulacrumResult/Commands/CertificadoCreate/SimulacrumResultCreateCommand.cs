

using Domain.Entity.QuestionResponseSimulacrum;

namespace Application.Service.SimulacrumResult.Commands.SimulacrumResultCreate
{
    public class SimulacrumResultCreateInputCommand
    {
        public string IdSimulacro { get; set; }
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

        public SimulacrumResultCreateInputCommand()
        {

        }


        public SimulacrumResultCreateInputCommand(string idSimulacro, int duracion, DateTime fecha, int numCorrectasCiudadanas, int numCorrectasIngles, int numCorrectasRazonamiento, int numCorrectasLectura, int totalCiudadanas, int totalLectura, int totalIngles, int totalRazonamiento, double puntaje, List<QuestionResponseSimulacrumEntity> questionsResponses)
        {
            IdSimulacro = idSimulacro;
            Duracion = duracion;
            Fecha = fecha;
            NumCorrectasCiudadanas = numCorrectasCiudadanas;
            NumCorrectasIngles = numCorrectasIngles;
            NumCorrectasRazonamiento = numCorrectasRazonamiento;
            NumCorrectasLectura = numCorrectasLectura;
            TotalCiudadanas = totalCiudadanas;
            TotalLectura = totalLectura;
            TotalIngles = totalIngles;
            TotalRazonamiento = totalRazonamiento;
            Puntaje = puntaje;
            QuestionsResponses = questionsResponses;
        }
    }


}