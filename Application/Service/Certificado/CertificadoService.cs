
using Application.Service.Certificado.Commands.CertificadoCreate;
using Application.Service.Certificado.Queries.CertificadoGetAll;
using Application.Service.Certificado.Queries.GetRankingResult;
using Domain.Base.ResponseEntity;
using Domain.Entity;
using Domain.Entity.RankingResponseEntity;
using Domain.Entity.Simulacros;
using Domain.Port.Certificado;

namespace Application.Service.Certificado
{
    public class CertificadoService
    {
        private readonly ICertificadoSimulacroRepository _repository;
        public CertificadoService(ICertificadoSimulacroRepository repository) => _repository = repository;

        public async Task<bool> CreateDefault(CertificadoCreateInputCommand command, string idUser)
        {
            var _create = new CertificadoCreateCommandHandler(_repository);
            return await _create.HandleAsync(command, idUser, "default");
        }

        public async Task<bool> CreatePersonalized(CertificadoCreateInputCommand command, string idUser)
        {
            var _create = new CertificadoCreateCommandHandler(_repository);
            return await _create.HandleAsync(command, idUser, "personalized");
        }

        public async Task<List<SimulacroResultEntity>> GetAll(string idUser)
        {
            var _getAll = new CertificadoGetAllQueriesHandler(_repository);
            return await _getAll.HandleAsync(idUser);
        }

        public async Task<(RankingResponseEntity<SimulacroResultEntity>, List<UserEntity>)> GetRanking(GetRankingResultInputQuery query, string idUser)
        {
            var _getAll = new GetRankingResultQueriesHandler(_repository);
            return await _getAll.HandleAsync(query, idUser);
        }


    }
}