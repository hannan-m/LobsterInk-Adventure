using System;
using System.Collections.Generic;
using System.Reflection;
using FluentValidation;
using LobsterInk.Application.Adventures;
using LobsterInk.Application.Adventures.Models;
using LobsterInk.Application.Common.Interfaces;
using LobsterInk.Domain.Entities;
using LobsterInk.Domain.Enums;
using LobsterInk.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LobsterInk.Application.UnitTests
{
    public class DependencySetupFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public Domain.Entities.Adventure Adventure { get; private set; }

        public DependencySetupFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddDbContext<IApplicationDbContext, ApplicationDbContext>(
                    options => options.UseInMemoryDatabase($"{Guid.NewGuid().ToString()}.db"),
                    ServiceLifetime.Transient);
            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
            serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            serviceCollection.AddTransient<IAdventureService, AdventureService>();
            serviceCollection.AddTransient<IAdventureQuestionService, AdventureQuestionService>();
            serviceCollection.AddTransient<IAdventureQuestionHistoryService, AdventureQuestionHistoryService>();


            ServiceProvider = serviceCollection.BuildServiceProvider();

            var adventureId = Guid.NewGuid().ToString();
            Adventure = new Domain.Entities.Adventure
            {
                Id = adventureId,
                Name = "Doughnut decision helper",
                Questions = new List<AdventureQuestion>
                {
                    new AdventureQuestion
                    {
                        Id = Guid.NewGuid().ToString(), AdventureId = adventureId, Question = "Do I want a Doughnut?",
                        Type = QuestionType.No,
                        Children = new List<AdventureQuestion>(new[]
                        {
                            new AdventureQuestion
                            {
                                Id = Guid.NewGuid().ToString(), AdventureId = adventureId,
                                Question = "Maybe you want an apple?",
                                Type = QuestionType.No
                            },
                            new AdventureQuestion
                            {
                                Id = Guid.NewGuid().ToString(), AdventureId = adventureId,
                                Question = "Do I deserve it?", Type = QuestionType.Yes,
                                Children = new List<AdventureQuestion>(new[]
                                {
                                    new AdventureQuestion
                                    {
                                        Id = Guid.NewGuid().ToString(), AdventureId = adventureId,
                                        Question = "Is it a a good doughnut?",
                                        Type = QuestionType.No,
                                        Children = new List<AdventureQuestion>(new[]
                                        {
                                            new AdventureQuestion
                                            {
                                                Id = Guid.NewGuid().ToString(), AdventureId = adventureId,
                                                Question = "Wait 'til you find a sinful, unforgettable doughnut.",
                                                Type = QuestionType.No
                                            },
                                            new AdventureQuestion
                                            {
                                                Id = Guid.NewGuid().ToString(), AdventureId = adventureId,
                                                Question = "What are you waiting for? Grab it now.",
                                                Type = QuestionType.Yes
                                            }
                                        })
                                    },
                                    new AdventureQuestion
                                    {
                                        Id = Guid.NewGuid().ToString(), AdventureId = adventureId,
                                        Question = "Are you sure?",
                                        Type = QuestionType.Yes,
                                        Children = new List<AdventureQuestion>(new[]
                                        {
                                            new AdventureQuestion
                                            {
                                                Id = Guid.NewGuid().ToString(), AdventureId = adventureId,
                                                Question = "Do jumping jacks first.",
                                                Type = QuestionType.No
                                            },
                                            new AdventureQuestion
                                            {
                                                Id = Guid.NewGuid().ToString(), AdventureId = adventureId,
                                                Question = "Get it.",
                                                Type = QuestionType.Yes
                                            }
                                        })
                                    }
                                })
                            }
                        })
                    }
                }
            };
            var context = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Adventures.Add(Adventure);
            context.SaveChanges();
        }
    }
}