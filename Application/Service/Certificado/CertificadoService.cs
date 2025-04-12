
using Application.Service.Certificado.Commands.CertificadoCreate;
using Application.Service.Certificado.Queries.CertificadoGetAll;
using Domain.Base.ResponseEntity;
using Domain.Entity.Simulacros;
using Domain.Port.Certificado;

namespace Application.Service.Certificado
{
    public class CertificadoService
    {
        private readonly ICertificadoSimulacroRepository _repository;
        public CertificadoService(ICertificadoSimulacroRepository repository) => _repository = repository;

        public async Task<bool> Create(CertificadoCreateInputCommand command, string idUser)
        {
            var _create = new CertificadoCreateCommandHandler(_repository);
            return await _create.HandleAsync(command, idUser);
        }

        public async Task<List<SimulacroResultEntity>> GetAll(string idUser)
        {
            var _getAll = new CertificadoGetAllQueriesHandler(_repository);
            return await _getAll.HandleAsync(idUser);
        }


    }
}