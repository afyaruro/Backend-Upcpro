
using Domain.Entity;
using Domain.Entity.RankingResponseEntity;
using Domain.Entity.Simulacros;
using Domain.Port.Certificado;

namespace Application.Service.Certificado.Queries.GetRankingResult
{
    public class GetRankingResultQueriesHandler
    {
        private readonly ICertificadoSimulacroRepository _certificadoRepository;

        public GetRankingResultQueriesHandler(ICertificadoSimulacroRepository certificadoRepository)
        {
            this._certificadoRepository = certificadoRepository;
        }

        public async Task<(RankingResponseEntity<SimulacroResultEntity>, List<UserEntity>)> HandleAsync(GetRankingResultInputQuery query, string idUser)
        {
            return await this._certificadoRepository.GetRankingByScore(idUser, query.IdSimulacro);
        }


    }
}

