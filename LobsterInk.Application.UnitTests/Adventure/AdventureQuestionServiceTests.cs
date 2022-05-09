using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LobsterInk.Application.Adventures.ViewModels;
using LobsterInk.Application.Common.Exceptions;
using LobsterInk.Application.Common.Interfaces;
using LobsterInk.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LobsterInk.Application.UnitTests.Adventure
{
    public class AdventureQuestionServiceTests : IClassFixture<DependencySetupFixture>
    {
        private readonly IAdventureQuestionService _questionService;

        private readonly Domain.Entities.Adventure _adventure;

        public AdventureQuestionServiceTests(DependencySetupFixture fixture)
        {
            _questionService = fixture.ServiceProvider.GetRequiredService<IAdventureQuestionService>();
            _adventure = fixture.Adventure;
        }

        [Fact]
        public async Task FirstAdventureQuestion_ByAdventureId_ShouldReturnAQuestion()
        {
            var question = await _questionService.GetFirstByAdventureId(_adventure.Id);
            question.Should().NotBeNull();
            question.Question.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task FirstAdventureQuestion_ByAdventureId_ShouldReturnThrowNotFoundException_WhenIdNotFound()
        {
            Func<Task> act = () => _questionService.GetFirstByAdventureId(Guid.NewGuid().ToString());
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task AdventureQuestion_ById_ShouldReturnAQuestion()
        {
            var questionById = await _questionService.GetById(_adventure.Questions.First().Id);
            questionById.Should().NotBeNull();
            questionById.Question.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task AdventureQuestion_ById_ShouldReturnThrowNotFoundException_WhenIdNotFound()
        {
            Func<Task> act = () => _questionService.GetById(Guid.NewGuid().ToString());
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task NextAdventureQuestion_ByIdAndQuestionType_ShouldReturnAQuestion()
        {
            var nextQuestion = await _questionService.GetNext(_adventure.Questions.First().Id, QuestionType.Yes);
            nextQuestion.Should().NotBeNull();
            nextQuestion.Question.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task NextAdventureQuestion_ByIdAndQuestionType_ShouldThrowNotFoundException_WhenQuestionByIdNotExist()
        {
            Func<Task> act = async () => await _questionService.GetNext(Guid.NewGuid().ToString(), QuestionType.Yes);
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task FirstQuestion_ById_ShouldThrowNotFoundException_WhenIdNotFound()
        {
            Func<Task> act = () => _questionService.GetById(Guid.NewGuid().ToString());
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task FirstQuestion_ByAdventureId_ShouldThrowNotFoundException_WhenIdNotFound()
        {
            Func<Task> act = () => _questionService.GetFirstByAdventureId(Guid.NewGuid().ToString());
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}