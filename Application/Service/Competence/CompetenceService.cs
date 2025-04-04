using Application.Service.Competence.Commands.CompetenceCreate;
using Application.Service.Competence.Commands.CompetenceDelete;
using Application.Service.Competence.Commands.CompetenceGetAllPage;
using Application.Service.Competence.Commands.CompetenceUpdate;
using Domain.Base.ResponseEntity;
using Domain.Port.Competence;

namespace Application.Service.Competence
{
    public class CompetenceService
    {
        private readonly ICompetenceRepository _repository;
        public CompetenceService(ICompetenceRepository repository) => _repository = repository;

        public async Task<CreateOutputCompetenceCommand> Create(CreateInputCompetenceCommand command)
        {
            var _create = new CreateCompetenceCommandHandler(_repository);
            return await _create.HandleAsync(command);
        }

        public async Task<ResponseEntity<GetAllPageCompetenceOutputCommand>> GetAllPage(GetAllPageCompetenceInputCommand command)
        {
            var _getAll = new GetAllPageCompetenceCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }

        public async Task<bool> Update(UpdateCompetenceCommand command)
        {
            var _update = new UpdateCompetenceCommandHandler(_repository);
            return await _update.HandleAsync(command);
        }

        public async Task<bool> Delete(DeleteCompetenceCommand command)
        {
            var _delete = new DeleteCompetenceCommandHandler(_repository);
            return await _delete.HandleAsync(command);
        }


    }
}