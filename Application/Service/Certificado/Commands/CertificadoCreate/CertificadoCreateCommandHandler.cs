
using Application.Common.Exceptions;
using Domain.Entity.Simulacros;
using Domain.Port.Certificado;
using FluentValidation;

namespace Application.Service.Certificado.Commands.CertificadoCreate
{
    public class CertificadoCreateCommandHandler
    {
        private readonly ICertificadoSimulacroRepository _certificadoRepository;

        public CertificadoCreateCommandHandler(ICertificadoSimulacroRepository certificadoRepository)
        {
            this._certificadoRepository = certificadoRepository;
        }

        public async Task<bool> HandleAsync(CertificadoCreateInputCommand command, string idUser, string typeResult)
        {

            var validator = new CertificadoCreateCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (await _certificadoRepository.ExistByUser(idUser: idUser, idSimulacro: command.IdSimulacro))
            {
                throw new EntityExistException("Ya existe un certificado para este simulacro");
            }


            return await _certificadoRepository.CrearAsync(new SimulacroResultEntity(
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
                jsonQuestions: command.JsonQuestions

            ));
        }
    }
}

