using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain.Base.ResponseEntity;
using Domain.Entity.Question;
using Domain.Port;
using FluentValidation;

namespace Application.Service.InfoQuestion.Commands.InfoQuestionGetAllPage
{
    public class GetAllPageInfoQuestionCommandHandler
    {
        private readonly IQuestionRepository<InfoQuestionEntity> _InfoQuestionRepository;

        public GetAllPageInfoQuestionCommandHandler(IQuestionRepository<InfoQuestionEntity> InfoQuestionRepository)
        {
            this._InfoQuestionRepository = InfoQuestionRepository;
        }

        public async Task<ResponseEntity<GetAllPageInfoQuestionOutputCommand>> HandleAsync(GetAllPageInfoQuestionInputCommand command)
        {

            var validator = new GetAllPageInfoQuestionCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return ResponseEntityToResponseCommands(await this._InfoQuestionRepository.GetAll(page: command.Page, pageSize: command.Size));
        }

        private ResponseEntity<GetAllPageInfoQuestionOutputCommand> ResponseEntityToResponseCommands(ResponseEntity<InfoQuestionEntity> resp)
        {
            var responseCommands = new ResponseEntity<GetAllPageInfoQuestionOutputCommand>();
            responseCommands.totalPages = resp.totalPages;
            responseCommands.totalRecords = resp.totalRecords;
            responseCommands.message = resp.message;
            responseCommands.isError = resp.isError;
            responseCommands.listEntity = new List<GetAllPageInfoQuestionOutputCommand>();
            foreach (var entity in resp.listEntity!)
            {
                var command = new GetAllPageInfoQuestionOutputCommand();
                command.Contexto = entity.Contexto;
                command.Fuente = entity.Fuente;
                command.TypeQuestion = entity.TypeQuestion;
                command.Id = entity.Id;
                responseCommands.listEntity.Add(command);
            }

            return responseCommands;

        }
    }
}

