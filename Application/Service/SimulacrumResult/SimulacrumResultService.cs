
using Application.Service.SimulacrumResult.Commands.SimulacrumResultCreate;
using Application.Service.SimulacrumResult.Queries.SimulacrumResultGetAll;
using Domain.Base.ResponseEntity;
using Domain.Entity.Simulacros;
using Domain.Port.SimulacrumResult;

namespace Application.Service.SimulacrumResult
{
    public class SimulacrumResultService
    {
        private readonly ISimulacrumResultRepository _repository;
        public SimulacrumResultService(ISimulacrumResultRepository repository) => _repository = repository;

        public async Task<bool> CreateDefault(SimulacrumResultCreateInputCommand command, string idUser)
        {
            var _create = new SimulacrumResultCreateCommandHandler(_repository);
            return await _create.HandleAsync(command, idUser, "default");
        }


        public async Task<List<SimulacrumResultEntity>> GetAll(string idUser)
        {
            var _getAll = new SimulacrumResultGetAllQueriesHandler(_repository);
            return await _getAll.HandleAsync(idUser);
        }




    }
}