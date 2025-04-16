using Application.Service.InfoQuestion.Commands.InfoQuestionCreate;
using Application.Service.InfoQuestion.Commands.InfoQuestionDelete;
using Application.Service.InfoQuestion.Commands.InfoQuestionGetAllPage;
using Application.Service.InfoQuestion.Commands.InfoQuestionUpdate;
using Domain.Base.ResponseEntity;
using Domain.Entity.Question;
using Domain.Port;

namespace Application.Service.InfoQuestion
{
    public class InfoQuestionService
    {
        private readonly IQuestionRepository<InfoQuestionEntity> _repository;
        public InfoQuestionService(IQuestionRepository<InfoQuestionEntity> repository) => _repository = repository;

        public async Task<InfoQuestionCreateOutputCommand> Create(InfoQuestionCreateInputCommand command)
        {
            var _create = new InfoQuestionCreateCommandHandler(_repository);
            return await _create.HandleAsync(command);
        }

        public async Task<ResponseEntity<InfoQuestionGetAllPageOutputCommand>> GetAllPage(InfoQuestionGetAllPageInputCommand command)
        {
            var _getAll = new InfoQuestionGetAllPageCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }

        public async Task<bool> Update(InfoQuestionUpdateInputCommand command)
        {
            var _update = new InfoQuestionUpdateCommandHandler(_repository);
            return await _update.HandleAsync(command);
        }

        public async Task<bool> Delete(InfoQuestionDeleteInputCommand command)
        {
            var _delete = new InfoQuestionDeleteCommandHandler(_repository);
            return await _delete.HandleAsync(command);
        }

        public async Task<ResponseEntity<InfoQuestionGetAllPageOutputCommand>> GetAllSync(InfoQuestionGetAllPageSyncInputCommand command)
        {
            var _getAll = new InfoQuestionGetAllPageSyncCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }


    }
}