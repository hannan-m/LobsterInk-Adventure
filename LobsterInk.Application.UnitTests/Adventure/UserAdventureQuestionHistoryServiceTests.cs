using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.TestHelper;
using LobsterInk.Application.Adventures;
using LobsterInk.Application.Adventures.Models;
using LobsterInk.Application.Adventures.Validators;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LobsterInk.Application.UnitTests.Adventure;

public class UserAdventureQuestionHistoryServiceTests : IClassFixture<DependencySetupFixture>
{
    private readonly IAdventureQuestionHistoryService _historyService;
    private readonly CreateUserAdventureQuestionHistoryModelValidator _adventureQuestionHistoryModelValidator;
    private readonly Domain.Entities.Adventure _adventure;
    private readonly string _userId = Guid.NewGuid().ToString();

    public UserAdventureQuestionHistoryServiceTests(DependencySetupFixture fixture)
    {
        _historyService = fixture.ServiceProvider.GetRequiredService<IAdventureQuestionHistoryService>();
        _adventure = fixture.Adventure;
        _adventureQuestionHistoryModelValidator = new CreateUserAdventureQuestionHistoryModelValidator();
    }

    [Fact]
    public async Task AddHistory_ShouldCreateNewRecord_WhenDataIdsAreValid()
    {
        await _historyService.Add(_adventure.Questions.First().Id, _userId);

        var history = await _historyService.GetByAdventureId(_adventure.Id, _userId);
        history.Should().NotBeNull();
        history.Count.Should().Be(1);
    }

    [Fact]
    public async Task AddHistory_ShouldThrowDuplicateRecordException_WhenDataIdsAreValidAndAlreadyExist()
    {
        var adventureId = _adventure.Id;
        await _historyService.Add(_adventure.Questions.First().Id, _userId);

        var history = await _historyService.GetByAdventureId(adventureId, _userId);
        history.Should().NotBeNull();
        history.Count.Should().Be(1);
    }

    [Fact]
    public void AddHistory_ShouldThrowValidationErrors_WhenDataIdsAreInvalid()
    {
        var model = new CreateUserAdventureQuestionHistoryModel
        {
            AdventureQuestionId = "not a guid",
            UserId = "not a guid"
        };

        var result = _adventureQuestionHistoryModelValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(customer => customer.AdventureQuestionId);
        result.ShouldHaveValidationErrorFor(customer => customer.UserId);
    }
}