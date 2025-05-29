using Application.Service.EspacioFisico.Commands.EspacioFisicoGetAllPage;
using Domain.Base.ResponseEntity;
using Domain.Port.EspacioFisico;

namespace Application.Service.EspacioFisico
{
    public class EspacioFisicoService
    {
        private readonly IEspacioFisicoRepository _repository;
        public EspacioFisicoService(IEspacioFisicoRepository repository) => _repository = repository;


        public async Task<ResponseEntity<EspacioFisicoGetAllPageOutputCommand>> GetAllPage(EspacioFisicoGetAllPageInputCommand command)
        {
            var _getAll = new EspacioFisicoGetAllPageCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }


        public async Task<ResponseEntity<EspacioFisicoGetAllPageOutputCommand>> GetAllSync(EspacioFisicoGetAllPageSyncInputCommand command)
        {
            var _getAll = new EspacioFisicoGetAllPageSyncCommandHandler(_repository);
            return await _getAll.HandleAsync(command);
        }


    }
}