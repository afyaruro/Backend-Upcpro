
using Application.Service.Certificado;
using Application.Service.Competence;
using Application.Service.Faculty;
using Application.Service.InfoQuestion;
using Application.Service.Level;
using Application.Service.Program;
using Application.Service.Question;
using Application.Service.Simulacro;
using Application.Service.User;
using Domain.Entity.Question;
using Domain.Port;
using Domain.Port.Certificado;
using Domain.Port.Competence;
using Domain.Port.Faculty;
using Domain.Port.Level;
using Domain.Port.Program;
using Domain.Port.Simulacro;
using Domain.Port.User;
using Infrastructure.Adapters.Certificado;
using Infrastructure.Adapters.Competence;
using Infrastructure.Adapters.Faculty;
using Infrastructure.Adapters.InfoQuestion;
using Infrastructure.Adapters.Level;
using Infrastructure.Adapters.Program;
using Infrastructure.Adapters.Question;
using Infrastructure.Adapters.ResultLevel;
using Infrastructure.Adapters.Simulacro;
using Infrastructure.Adapters.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions.Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
        {

            services.AddControllers();

            services.AddSingleton<IProgramRepository, ProgramRepository>();
            services.AddScoped<ProgramService>();

            services.AddSingleton<IFacultyRepository, FacultyRepository>();
            services.AddScoped<FacultyService>();

            services.AddSingleton<ICompetenceRepository, CompetenceRepository>();
            services.AddScoped<CompetenceService>();

            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddScoped<UserService>();

            services.AddSingleton<IQuestionRepository<QuestionEntity>, QuestionRepository>();
            services.AddScoped<QuestionService>();

            services.AddSingleton<IQuestionRepository<InfoQuestionEntity>, InfoQuestionRepository>();
            services.AddScoped<InfoQuestionService>();

            services.AddSingleton<ILevelRepository, LevelRepository>();
            services.AddScoped<LevelService>();

            services.AddSingleton<IResultLevelRepository, ResultLevelRepository>();
            services.AddScoped<ResultLevelService>();

            services.AddSingleton<ISimulacroRepository, SimulacroRepository>();
            services.AddScoped<SimulacroService>();

            services.AddSingleton<ICertificadoSimulacroRepository, CertificadoSimulacroRepository>();
            services.AddScoped<CertificadoService>();


            return services;
        }
    }
}