
using Application.Common.Exceptions;
using Domain.Entity.Facultad;
using Domain.Port.Faculty;
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyCreate
{
    public class FacultyCreateCommandHandler
    {
        private readonly IFacultyRepository _FacultyRepository;

        public FacultyCreateCommandHandler(IFacultyRepository FacultyRepository)
        {
            this._FacultyRepository = FacultyRepository;
        }

        public async Task<FacultyCreateOutputCommand> HandleAsync(FacultyCreateInputCommand command)
        {

            var validator = new FacultyCreateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            command.Name = command.Name.ToUpper();

            if (await _FacultyRepository.ExistByName(command.Name))
            {
                throw new EntityExistException("La facultad ya existe");
            }

            var resp = await this._FacultyRepository.Add(new FacultyEntity(name: command.Name));

            return new FacultyCreateOutputCommand(resp.Name, resp.Id);
        }
    }
}

