using Domain.Base.ResponseEntity;
using Domain.Entity.EspacioFisico;
using Domain.Port.EspacioFisico;
using FluentValidation;

namespace Application.Service.EspacioFisico.Commands.EspacioFisicoGetAllPage
{
    public class EspacioFisicoGetAllPageSyncCommandHandler
    {
        private readonly IEspacioFisicoRepository _EspacioFisicoRepository;

        public EspacioFisicoGetAllPageSyncCommandHandler(IEspacioFisicoRepository EspacioFisicoRepository)
        {
            this._EspacioFisicoRepository = EspacioFisicoRepository;
        }

        public async Task<ResponseEntity<EspacioFisicoGetAllPageOutputCommand>> HandleAsync(EspacioFisicoGetAllPageSyncInputCommand command)
        {

            var validator = new EspacioFisicoGetAllPageSyncCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return ResponseEntityToResponseCommands(await this._EspacioFisicoRepository.GetAll(command.LateDateSync));
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

