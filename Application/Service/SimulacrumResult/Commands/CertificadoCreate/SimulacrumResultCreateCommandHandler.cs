
using Application.Common.Exceptions;
using Domain.Entity.Simulacros;
using Domain.Port.SimulacrumResult;
using FluentValidation;

namespace Application.Service.SimulacrumResult.Commands.SimulacrumResultCreate
{
    public class SimulacrumResultCreateCommandHandler
    {
        private readonly ISimulacrumResultRepository _simulacrumResultRepository;

        public SimulacrumResultCreateCommandHandler(ISimulacrumResultRepository simulacrumResultRepository)
        {
            this._simulacrumResultRepository = simulacrumResultRepository;
        }

        public async Task<bool> HandleAsync(SimulacrumResultCreateInputCommand command, string idUser, string typeResult)
        {

            var validator = new SimulacrumResultCreateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }


            if (await _simulacrumResultRepository.ExistByUser(idUser: idUser, idSimulacro: command.IdSimulacro))
            {
                throw new EntityExistException("Ya existe un SimulacrumResult para este simulacro");
            }

            return await _simulacrumResultRepository.CrearAsync(new SimulacrumResultEntity(
                type: typeResult,
                idSimulacro: command.IdSimulacro,
                idEstudiante: idUser,
                duracion: command.Duracion,
                fecha: command.Fecha,
                numCorrectasCiudadanas: command.NumCorrectasCiudadanas,
                numCorrectasIngles: command.NumCorrectasIngles,
                numCorrectasRazonamiento: command.NumCorrectasRazonamiento,
                numCorrectasLectura: command.NumCorrectasLectura,
                totalCiudadanas: command.TotalCiudadanas,
                totalIngles: command.TotalIngles,
                totalRazonamiento: command.TotalRazonamiento,
                totalLectura: command.TotalLectura,
                puntaje: command.Puntaje,
                questionsResponses: command.QuestionsResponses
            ));
        }
    }
}

