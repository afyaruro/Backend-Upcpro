
using Application.Common.Exceptions;
using Domain.Entity.Facultad;
using Domain.Port.Faculty;
using FluentValidation;

namespace Application.Service.Faculty.Commands.FacultyUpdate
{
    public class UpdateFacultyCommandHandler
    {
        private readonly IFacultyRepository _facultyRepository;

        public UpdateFacultyCommandHandler(IFacultyRepository facultyRepository)
        {
            this._facultyRepository = facultyRepository;
        }

        public async Task<bool> HandleAsync(UpdateFacultyCommand command)
        {

            var validator = new UpdateFacultyCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var faculty = new FacultyEntity
            {
                Name = command.Name

            };

            faculty.Id = command.Id;
            faculty.Name = faculty.Name.ToUpper();
            faculty.DateUpdate = DateTime.Now;



            if (!await _facultyRepository.ExistById(faculty.Id))
            {
                throw new EntityNotFoundException("La competencia a actualizar no existe");
            }

            if (await _facultyRepository.ExistByName(faculty.Name))
            {
                throw new EntityExistException("El nuevo nombre de la competencia a actualizar ya existe");
            }

            return await this._facultyRepository.Update(faculty);
        }
    }
}

