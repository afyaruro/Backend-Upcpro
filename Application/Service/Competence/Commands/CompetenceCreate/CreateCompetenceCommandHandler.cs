using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Service.Competence.Commands.CompetenceCreate;
using Domain.Entity.Competence;
using Domain.Port.Competence;
using FluentValidation;
using Microsoft.AspNetCore.Http.Internal;

namespace Application.Service.Competence.Commands.CompetenceCreate
{
    public class CreateCompetenceCommandHandler
    {
        private readonly ICompetenceRepository _competenceRepository;

        public CreateCompetenceCommandHandler(ICompetenceRepository competenceRepository)
        {
            this._competenceRepository = competenceRepository;
        }

        public async Task<CreateOutputCompetenceCommand> HandleAsync(CreateInputCompetenceCommand command)
        {

            var validator = new CreateCompetenceCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (await _competenceRepository.ExistByName(command.Name.ToUpper()))
            {
                throw new EntityExistException("La competencia ya existe");
            }

            var resp = await this._competenceRepository.Add(new CompetenceEntity(command.Name.ToUpper()));

            return new CreateOutputCompetenceCommand(resp.Name, resp.Id);
        }
    }
}

