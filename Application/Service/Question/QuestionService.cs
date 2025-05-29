
using Application.Service.Question.Commands.QuestionGetAllPage;
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
        

        public async Task<ResponseEntity<QuestionGetAllPageOutputCommand>> GetAllPage(QuestionGetAllPageInputCommand command)
        {
            var _getAll = new QuestionGetAllPageCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }

        
        public async Task<ResponseEntity<QuestionGetAllPageOutputCommand>> GetAllSync(QuestionGetAllPageSyncInputCommand command)
        {
            var _getAll = new QuestionGetAllPageSyncCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }


    }
}