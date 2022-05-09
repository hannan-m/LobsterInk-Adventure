using FluentValidation;
using LobsterInk.Application.Adventures.Models;

namespace LobsterInk.Application.Adventures.Validators;

public class CreateAdventureQuestionModelValidator : AbstractValidator<CreateAdventureQuestionModel>
{
    public CreateAdventureQuestionModelValidator()
    {
        RuleFor(m => m.Type).IsInEnum();
        RuleFor(m => m.Question).NotEmpty().MaximumLength(150);
    }
}