

namespace Application.Service.Certificado.Commands.CertificadoCreate
{
    public class CertificadoCreateInputCommand
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
        public string JsonQuestions { get; set; }

        public CertificadoCreateInputCommand() { 
            
        }


        public CertificadoCreateInputCommand(string idSimulacro, int duracion, DateTime fecha, int numCorrectasCiudadanas, int numCorrectasIngles, int numCorrectasRazonamiento, int numCorrectasLectura, int totalCiudadanas, int totalLectura, int totalIngles, int totalRazonamiento, double puntaje, string jsonQuestions)
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
            JsonQuestions = jsonQuestions;
        }
    }


}