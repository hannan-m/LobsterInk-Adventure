using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using LobsterInk.Application.Adventures.Models;
using LobsterInk.Application.Adventures.ViewModels;
using LobsterInk.Application.Common.Exceptions;
using LobsterInk.Application.Common.Interfaces;
using LobsterInk.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LobsterInk.Application.UnitTests.Adventure
{
    public class AdventureServiceTests : IClassFixture<DependencySetupFixture>
    {
        private readonly IAdventureService _adventureService;
        private Domain.Entities.Adventure _adventure;
        public AdventureServiceTests(DependencySetupFixture fixture)
        {
            _adventureService = fixture.ServiceProvider.GetRequiredService<IAdventureService>();
            _adventure = fixture.Adventure;
        }

        [Fact]
        public async Task Adventure_ById_ShouldThrowNotFoundException_WhenIdNotFound()
        {
            Func<Task> act =  () => _adventureService.GetById(Guid.NewGuid().ToString());

            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task AdventureQuestions_ByAdventureId_ShouldThrowNotFoundException_WhenAdventureIdNotFound()
        {
            Func<Task> act = () => _adventureService.GetByIdWithQuestion(Guid.NewGuid().ToString());

            await act.Should().ThrowAsync<NotFoundException>();
        }


        public async Task InitialAdventureCount_ShouldBeOne()
        {
            var adventures = await _adventureService.List();
            adventures.Count.Should().Be(1);
        }

        [Fact]
        public async Task Adventure_ById_ShouldReturnOneRecord()
        {
            var adventureId = _adventure.Id;
            var adventureQuestion = await _adventureService.GetById(adventureId);
            adventureQuestion.Should().NotBeNull();
            adventureQuestion.Name.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task AdventureQuestions_ByAdventureId_ShouldReturnQuestions()
        {
            var adventureId = _adventure.Id;
            var adventureQuestions = await _adventureService.GetByIdWithQuestion(adventureId);
            adventureQuestions.ToList().Count.Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async Task GivenAdventureTree_ShouldCreateNewAdventure_WhenDataIsValid()
        {
            var adventure = new CreateAdventureModel
            {
                Name = "Rent a Car",
                Questions = new List<CreateAdventureQuestionModel>
                {
                    new CreateAdventureQuestionModel
                    {
                        Question = "Are you older than 18?", Type = QuestionType.No,
                        Children = new List<CreateAdventureQuestionModel>(new[]
                        {
                            new CreateAdventureQuestionModel
                                {Question = "Sorry, you are not old enough?", Type = QuestionType.No},
                            new CreateAdventureQuestionModel
                            {
                                Question = "Do you have driving license?", Type = QuestionType.Yes,
                                Children = new List<CreateAdventureQuestionModel>(new[]
                                {
                                    new CreateAdventureQuestionModel
                                    {
                                        Question = "The license is not valid!!", Type = QuestionType.No,
                                    },
                                    new CreateAdventureQuestionModel
                                    {
                                        Question = "Do you have 100$?", Type = QuestionType.Yes,
                                        Children = new List<CreateAdventureQuestionModel>(new[]
                                        {
                                            new CreateAdventureQuestionModel
                                            {
                                                Question = "Go get the money first!",
                                                Type = QuestionType.No
                                            },
                                            new CreateAdventureQuestionModel
                                            {
                                                Question = "Great, here's your rental details.", Type = QuestionType.Yes
                                            }
                                        })
                                    }
                                })
                            }
                        })
                    }
                }
            };

            var createdId = await _adventureService.CreateAdventure(adventure);

            createdId.Should().NotBeNullOrEmpty();
            createdId.Should().BeOfType<string>();
            var isValidGUID = Guid.TryParse(createdId, out var guid);
            isValidGUID.Should().Be(true);
            guid.Should().Be(createdId);

            var newAdventures = await _adventureService.List();
            newAdventures.Count.Should().Be(2);
        }
    }
}