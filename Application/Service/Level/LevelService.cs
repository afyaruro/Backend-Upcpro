
using Application.Service.Level.Commands.LevelCreate;
using Application.Service.Level.Commands.LevelDelete;
using Application.Service.Level.Commands.LevelUpdate;
using Application.Service.Faculty.Commands.FacultyGetAllPage;
using Domain.Base.ResponseEntity;
using Domain.Port.Level;

namespace Application.Service.Level
{
    public class LevelService
    {
        private readonly ILevelRepository _repository;
        public LevelService(ILevelRepository repository) => _repository = repository;

        public async Task<LevelOutputCreateCommand> Create(LevelCreateInputCommand command)
        {
            var _create = new LevelCreateCommandHandler(_repository);
            return await _create.HandleAsync(command);
        }

        public async Task<ResponseEntity<LevelGetAllPageOutputCommand>> GetAllPage(LevelGetAllPageInputCommand command)
        {
            var _getAll = new LevelGetAllPageCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }

        public async Task<bool> Update(LevelUpdateInputCommand command)
        {
            var _update = new LevelUpdateCommandHandler(_repository);
            return await _update.HandleAsync(command);
        }

        public async Task<bool> Delete(LevelDeleteInputCommand command)
        {
            var _delete = new LevelDeleteCommandHandler(_repository);
            return await _delete.HandleAsync(command);
        }

        


    }
}