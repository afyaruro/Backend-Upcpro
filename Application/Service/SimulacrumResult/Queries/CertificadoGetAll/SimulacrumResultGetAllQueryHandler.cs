
using Domain.Entity.Simulacros;
using Domain.Port.SimulacrumResult;

namespace Application.Service.SimulacrumResult.Queries.SimulacrumResultGetAll
{
    public class SimulacrumResultGetAllQueriesHandler
    {
        private readonly ISimulacrumResultRepository _SimulacrumResultRepository;

        public SimulacrumResultGetAllQueriesHandler(ISimulacrumResultRepository SimulacrumResultRepository)
        {
            this._SimulacrumResultRepository = SimulacrumResultRepository;
        }

        public async Task<List<SimulacrumResultEntity>> HandleAsync(string idUser)
        {

            return await this._SimulacrumResultRepository.GetAll(idUser);
        }

       
    }
}

