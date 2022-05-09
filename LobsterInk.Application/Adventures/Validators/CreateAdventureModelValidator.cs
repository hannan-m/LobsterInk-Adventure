using FluentValidation;
using LobsterInk.Application.Adventures.Models;

namespace LobsterInk.Application.Adventures.Validators
{
    public class CreateAdventureModelValidator : AbstractValidator<CreateAdventureModel>
    {
        public CreateAdventureModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().MaximumLength(150);

            RuleForEach(model => model.Questions).SetValidator(new CreateAdventureQuestionModelValidator());
        }
    }
}