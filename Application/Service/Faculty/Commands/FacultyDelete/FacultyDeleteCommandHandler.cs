
using Application.Common.Exceptions;
using Domain.Port.Faculty;
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyDelete
{
    public class FacultyDeleteCommandHandler
    {
        private readonly IFacultyRepository _FacultyRepository;

        public FacultyDeleteCommandHandler(IFacultyRepository FacultyRepository)
        {
            this._FacultyRepository = FacultyRepository;
        }

        public async Task<bool> HandleAsync(FacultyDeleteInputCommand command)
        {

            var validator = new FacultyDelete.FacultyDeleteCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (!await _FacultyRepository.ExistById(command.Id))
            {
                throw new EntityNotFoundException("La facultad no existe");
            }



            return await this._FacultyRepository.Delete(command.Id);
        }
    }
}

