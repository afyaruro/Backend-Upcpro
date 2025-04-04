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

        public async Task<CreateOutputInfoQuestionCommand> Create(CreateInputInfoQuestionCommand command)
        {
            var _create = new CreateInfoQuestionCommandHandler(_repository);
            return await _create.HandleAsync(command);
        }

        public async Task<ResponseEntity<GetAllPageInfoQuestionOutputCommand>> GetAllPage(GetAllPageInfoQuestionInputCommand command)
        {
            var _getAll = new GetAllPageInfoQuestionCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }

        public async Task<bool> Update(UpdateInfoQuestionCommand command)
        {
            var _update = new UpdateInfoQuestionCommandHandler(_repository);
            return await _update.HandleAsync(command);
        }

        public async Task<bool> Delete(DeleteInfoQuestionCommand command)
        {
            var _delete = new DeleteInfoQuestionCommandHandler(_repository);
            return await _delete.HandleAsync(command);
        }


    }
}