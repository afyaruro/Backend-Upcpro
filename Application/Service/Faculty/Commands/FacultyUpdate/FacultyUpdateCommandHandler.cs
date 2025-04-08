
using Application.Common.Exceptions;
using Domain.Entity.Facultad;
using Domain.Port.Faculty;
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyUpdate
{
    public class FacultyUpdateCommandHandler
    {
        private readonly IFacultyRepository _facultyRepository;

        public FacultyUpdateCommandHandler(IFacultyRepository facultyRepository)
        {
            this._facultyRepository = facultyRepository;
        }

        public async Task<bool> HandleAsync(FacultyUpdateInputCommand command)
        {

            var validator = new FacultyUpdateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var faculty = new FacultyEntity(name: command.Name.ToUpper());
            faculty.Id = command.Id;
            faculty.DateUpdate = DateTime.Now;


            if (!await _facultyRepository.ExistById(faculty.Id))
            {
                throw new EntityNotFoundException("La competencia a falutad no existe");
            }

            if (await _facultyRepository.ExistByName(faculty.Name))
            {
                throw new EntityExistException("El nuevo nombre de la facultad a actualizar ya existe");
            }

            return await this._facultyRepository.Update(faculty);
        }
    }
}

