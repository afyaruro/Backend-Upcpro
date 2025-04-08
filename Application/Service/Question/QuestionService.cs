using Application.Service.Question.Commands.QuestionCreate;
using Application.Service.Question.Commands.QuestionDelete;
using Application.Service.Question.Commands.QuestionGetAllPage;
using Application.Service.Question.Commands.QuestionUpdate;
using Domain.Base.ResponseEntity;
using Domain.Entity.Question;
using Domain.Port;

namespace Application.Service.Question
{
    public class QuestionService
    {
        private readonly IQuestionRepository<QuestionEntity> _repository;
        private readonly IQuestionRepository<InfoQuestionEntity> _infoQuestionRepository;

        public QuestionService(IQuestionRepository<QuestionEntity> repository, IQuestionRepository<InfoQuestionEntity> infoQuestionRepository)
        {
            _repository = repository;
            _infoQuestionRepository = infoQuestionRepository;
        }
        public async Task<QuestionCreateOutputCommand> Create(QuestionCreateInputCommand command)
        {
            var _create = new QuestionCreateCommandHandler(_repository, _infoQuestionRepository);
            return await _create.HandleAsync(command);
        }

        public async Task<ResponseEntity<QuestionGetAllPageOutputCommand>> GetAllPage(QuestionGetAllPageInputCommand command)
        {
            var _getAll = new QuestionGetAllPageCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }

        public async Task<bool> Update(QuestionUpdateInputCommand command)
        {
            var _update = new QuestionUpdateCommandHandler(_repository);
            return await _update.HandleAsync(command);
        }

        public async Task<bool> Delete(QuestionDeleteInputCommand command)
        {
            var _delete = new QuestionDeleteCommandHandler(_repository);
            return await _delete.HandleAsync(command);
        }


    }
}