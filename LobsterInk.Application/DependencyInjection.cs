using System.Reflection;
using FluentValidation;
using LobsterInk.Application.Adventures;
using LobsterInk.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LobsterInk.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<IAdventureService, AdventureService>();
            services.AddTransient<IAdventureQuestionService, AdventureQuestionService>();
            return services;
        }
    }
}
