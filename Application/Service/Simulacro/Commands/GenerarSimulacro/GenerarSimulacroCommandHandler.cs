
using Application.Common.Exceptions;
using Domain.Port.Competence;
using Domain.Port.Simulacro;
using FluentValidation;

namespace Application.Service.Simulacro.Commands.GenerarSimulacro
{
    public class GenerarSimulacroCommandHandler
    {
        private readonly ISimulacroRepository _simulacroRepository;
        private readonly ICompetenceRepository _competenceRepository;

        public GenerarSimulacroCommandHandler(ISimulacroRepository simulacroRepository, ICompetenceRepository competenceRepository)
        {
            this._simulacroRepository = simulacroRepository;
            this._competenceRepository = competenceRepository;
        }

        public async Task<List<string>>  HandleAsync(GenerarSimulacroInputCommand command)
        {

            var validator = new GenerarSimulacroCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var simulacro = await _simulacroRepository.GetById(command.Id);
            if (simulacro == null)
            {
                throw new EntityNotFoundException("El Simulacro no existe");
            }

            var respCompetencias = await _competenceRepository.GetAll(page: 1, size: 4);
            if (respCompetencias.listEntity == null || respCompetencias.listEntity.Count == 0)
            {
                throw new EntityNotFoundException("No se encontraron competencias");
            }

            List<string> listPreguntas = [];

            foreach (var competencia in respCompetencias.listEntity)
            {
                
                var questionsCompetence = await _simulacroRepository.GenerateQuestionCompetence(numeroPreguntasByCompetence: simulacro.NumeroPreguntas / 4, idCompetence: competencia.Id);
                if (questionsCompetence != null && questionsCompetence.Count != 0)
                {
                    listPreguntas.AddRange(questionsCompetence);
                }
            }

            return listPreguntas;
        }
    }
}

