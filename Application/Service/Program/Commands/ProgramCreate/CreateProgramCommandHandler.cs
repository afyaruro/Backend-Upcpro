
using Application.Common.Exceptions;
using Application.Service.Faculty.Commands.FacultyCreate;
using Domain.Entity.Facultad;
using Domain.Entity.Program;
using Domain.Port.Faculty;
using Domain.Port.Program;
using FluentValidation;

namespace Application.Service.Program.Commands.ProgramCreate
{
    public class CreateProgramCommandHandler
    {
        private readonly IProgramRepository _programRepository;
        private readonly IFacultyRepository _facultadRepository;


        public CreateProgramCommandHandler(IProgramRepository programRepository, IFacultyRepository facultadRepository)
        {
            this._programRepository = programRepository;
            this._facultadRepository = facultadRepository;
        }

        public async Task<CreateOutputProgramCommand> HandleAsync(CreateInputProgramCommand command)
        {

            var validator = new CreateProgramCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (await _programRepository.ExistByName(command.Name.ToUpper()))
            {
                throw new EntityExistException("El programa ya existe");
            }

            if (!await _facultadRepository.ExistById(command.IdFaculty))
            {
                throw new EntityNotFoundException("la facultad no existe");
            }

            var resp = await this._programRepository.Add(new ProgramEntity(name: command.Name.ToUpper(), idFaculty: command.IdFaculty));

            return new CreateOutputProgramCommand(name: resp.Name, id: resp.Id, faculty: new CreateOutputFacultyCommand(id: resp.Faculty.Id, name: resp.Faculty.Name));
        }
    }
}

