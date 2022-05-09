using FluentValidation;
using LobsterInk.Application.Adventures.Models;

namespace LobsterInk.Application.Adventures.Validators;

public class
    CreateUserAdventureQuestionHistoryModelValidator : AbstractValidator<CreateUserAdventureQuestionHistoryModel>
{
    public CreateUserAdventureQuestionHistoryModelValidator()
    {
        RuleFor(m => m.AdventureQuestionId).NotEmpty().Must(s => Guid.TryParse(s, out _));
        RuleFor(m => m.UserId).NotEmpty().Must(s => Guid.TryParse(s, out _));
    }
}