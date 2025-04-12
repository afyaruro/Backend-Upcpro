
using Domain.Entity.Simulacros;
using Domain.Port.Certificado;

namespace Application.Service.Certificado.Queries.CertificadoGetAll
{
    public class CertificadoGetAllQueriesHandler
    {
        private readonly ICertificadoSimulacroRepository _certificadoRepository;

        public CertificadoGetAllQueriesHandler(ICertificadoSimulacroRepository certificadoRepository)
        {
            this._certificadoRepository = certificadoRepository;
        }

        public async Task<List<SimulacroResultEntity>> HandleAsync(string idUser)
        {

            return await this._certificadoRepository.GetAll(idUser);
        }

       
    }
}

