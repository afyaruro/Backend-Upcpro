
using Application.Common.Exceptions;
using Domain.Entity.Program;
using Domain.Port.Faculty;
using Domain.Port.Program;
using FluentValidation;

namespace Application.Service.Program.Commands.ProgramUpdate
{
    public class ProgramUpdateCommandHandler
    {
        private readonly IProgramRepository _programRepository;
        private readonly IFacultyRepository _facultadRepository;

        public ProgramUpdateCommandHandler(IProgramRepository programRepository, IFacultyRepository facultadRepository)
        {
            this._programRepository = programRepository;
            this._facultadRepository = facultadRepository;
        }

        public async Task<bool> HandleAsync(ProgramUpdateInputCommand command)
        {

            var validator = new ProgramUpdateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var Program = new ProgramEntity(name: command.Name.ToUpper(), idFaculty: command.IdFaculty);

            Program.Id = command.Id;
            Program.DateUpdate = DateTime.Now;


            if (!await _programRepository.ExistById(Program.Id))
            {
                throw new EntityNotFoundException("El programa a actualizar no existe");
            }

            var existProgramName = await _programRepository.ByName(Program.Name);

            if (existProgramName != null && existProgramName.Id != Program.Id)
            {

                throw new EntityExistException("El nuevo nombre del programa a actualizar ya existe");

            }

            if (!await _facultadRepository.ExistById(command.IdFaculty))
            {
                throw new EntityNotFoundException("la facultad no existe");
            }

            return await this._programRepository.Update(Program);
        }
    }
}

