

namespace Application.Service.Certificado.Queries.GetRankingResult
{
    public class GetRankingResultInputQuery
    {
        public string IdSimulacro { get; set; }


        public GetRankingResultInputQuery(string idSimulacro)
        {
            IdSimulacro = idSimulacro;
        }
    }


}