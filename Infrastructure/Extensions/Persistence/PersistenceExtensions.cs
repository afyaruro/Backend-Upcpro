
using Application.Service.Competence;
using Application.Service.Faculty;
using Application.Service.InfoQuestion;
using Application.Service.Program;
using Application.Service.User;
using Domain.Entity.Program;
using Domain.Entity.Question;
using Domain.Port;
using Domain.Port.Competence;
using Domain.Port.Faculty;
using Domain.Port.Generic;
using Domain.Port.Program;
using Domain.Port.User;
using Infrastructure.Adapters.Competence;
using Infrastructure.Adapters.Faculty;
using Infrastructure.Adapters.InfoQuestion;
using Infrastructure.Adapters.Program;
using Infrastructure.Adapters.Question;
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
            // services.AddScoped<QuestionService>();

            services.AddSingleton<IQuestionRepository<InfoQuestionEntity>, InfoQuestionRepository>();
            services.AddScoped<InfoQuestionService>();




            return services;
        }
    }
}