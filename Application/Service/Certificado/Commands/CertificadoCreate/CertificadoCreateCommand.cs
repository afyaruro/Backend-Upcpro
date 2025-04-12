

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

        public CertificadoCreateInputCommand(string name)
        {
            
        }

        public CertificadoCreateInputCommand(string idSimulacro, int duracion, DateTime fecha, int numCorrectasCiudadanas, int numCorrectasIngles, int numCorrectasRazonamiento, int numCorrectasLectura)
        {
            IdSimulacro = idSimulacro;
            Duracion = duracion;
            Fecha = fecha;
            NumCorrectasCiudadanas = numCorrectasCiudadanas;
            NumCorrectasIngles = numCorrectasIngles;
            NumCorrectasRazonamiento = numCorrectasRazonamiento;
            NumCorrectasLectura = numCorrectasLectura;
        }
    }


}