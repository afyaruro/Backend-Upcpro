
using Application.Service.InfoQuestion.Commands.InfoQuestionGetAllPage;
using Domain.Base.ResponseEntity;
using Domain.Entity.Question;
using Domain.Port;

namespace Application.Service.InfoQuestion
{
    public class InfoQuestionService
    {
        private readonly IQuestionRepository<InfoQuestionEntity> _repository;
        public InfoQuestionService(IQuestionRepository<InfoQuestionEntity> repository) => _repository = repository;



        public async Task<ResponseEntity<InfoQuestionGetAllPageOutputCommand>> GetAllPage(InfoQuestionGetAllPageInputCommand command)
        {
            var _getAll = new InfoQuestionGetAllPageCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }



        public async Task<ResponseEntity<InfoQuestionGetAllPageOutputCommand>> GetAllSync(InfoQuestionGetAllPageSyncInputCommand command)
        {
            var _getAll = new InfoQuestionGetAllPageSyncCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }


    }
}