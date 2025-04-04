
using Application.Common.Exceptions;
using Domain.Entity.Facultad;
using Domain.Port.Faculty;
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyCreate
{
    public class CreateFacultyCommandHandler
    {
        private readonly IFacultyRepository _FacultyRepository;

        public CreateFacultyCommandHandler(IFacultyRepository FacultyRepository)
        {
            this._FacultyRepository = FacultyRepository;
        }

        public async Task<CreateOutputFacultyCommand> HandleAsync(CreateInputFacultyCommand command)
        {

            var validator = new CreateFacultyCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (await _FacultyRepository.ExistByName(command.Name.ToUpper()))
            {
                throw new EntityExistException("La facultad ya existe");
            }

            var resp = await this._FacultyRepository.Add(new FacultyEntity(command.Name.ToUpper()));

            return new CreateOutputFacultyCommand(resp.Name, resp.Id);
        }
    }
}

