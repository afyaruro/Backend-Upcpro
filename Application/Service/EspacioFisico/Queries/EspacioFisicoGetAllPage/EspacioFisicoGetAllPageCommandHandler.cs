
using Domain.Base.ResponseEntity;
using Domain.Entity.EspacioFisico;
using Domain.Port.EspacioFisico;
using FluentValidation;

namespace Application.Service.EspacioFisico.Commands.EspacioFisicoGetAllPage
{
    public class EspacioFisicoGetAllPageCommandHandler
    {
        private readonly IEspacioFisicoRepository _EspacioFisicoRepository;

        public EspacioFisicoGetAllPageCommandHandler(IEspacioFisicoRepository EspacioFisicoRepository)
        {
            this._EspacioFisicoRepository = EspacioFisicoRepository;
        }

        public async Task<ResponseEntity<EspacioFisicoGetAllPageOutputCommand>> HandleAsync(EspacioFisicoGetAllPageInputCommand command)
        {

            var validator = new EspacioFisicoGetAllPageCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return ResponseEntityToResponseCommands(await this._EspacioFisicoRepository.GetAll(page: command.Page, size: command.Size));
        }

        private ResponseEntity<EspacioFisicoGetAllPageOutputCommand> ResponseEntityToResponseCommands(ResponseEntity<EspacioFisicoEntity> resp)
        {
            var responseCommands = new ResponseEntity<EspacioFisicoGetAllPageOutputCommand>();
            responseCommands.totalPages = resp.totalPages;
            responseCommands.totalRecords = resp.totalRecords;
            responseCommands.message = resp.message;
            responseCommands.isError = resp.isError;
            responseCommands.listEntity = new List<EspacioFisicoGetAllPageOutputCommand>();
            foreach (var entity in resp.listEntity!)
            {
                var command = new EspacioFisicoGetAllPageOutputCommand(name: entity.Name, id: entity.Id, dateUpdate: entity.DateUpdate);
                responseCommands.listEntity.Add(command);
            }

            return responseCommands;

        }
    }
}

