using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain.Base.ResponseEntity;
using Domain.Entity.Competence;
using Domain.Port.Competence;
using FluentValidation;

namespace Application.Service.Competence.Commands.CompetenceGetAllPage
{
    public class GetAllPageCompetenceCommandHandler
    {
        private readonly ICompetenceRepository _competenceRepository;

        public GetAllPageCompetenceCommandHandler(ICompetenceRepository competenceRepository)
        {
            this._competenceRepository = competenceRepository;
        }

        public async Task<ResponseEntity<GetAllPageCompetenceOutputCommand>> HandleAsync(GetAllPageCompetenceInputCommand command)
        {

            var validator = new GetAllPageCompetenceCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return ResponseEntityToResponseCommands(await this._competenceRepository.GetAll(page: command.Page, size: command.Size));
        }

        private ResponseEntity<GetAllPageCompetenceOutputCommand> ResponseEntityToResponseCommands(ResponseEntity<CompetenceEntity> resp)
        {
            var responseCommands = new ResponseEntity<GetAllPageCompetenceOutputCommand>();
            responseCommands.totalPages = resp.totalPages;
            responseCommands.totalRecords = resp.totalRecords;
            responseCommands.message = resp.message;
            responseCommands.isError = resp.isError;
            responseCommands.listEntity = new List<GetAllPageCompetenceOutputCommand>();
            foreach (var entity in resp.listEntity!)
            {
                var command = new GetAllPageCompetenceOutputCommand(name: entity.Name, id: entity.Id);
                responseCommands.listEntity.Add(command);
            }

            return responseCommands;

        }
    }
}

