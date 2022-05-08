using LobsterInk.Domain.Enums;

namespace LobsterInk.Application.Adventures.Models
{
    public class CreateAdventureQuestionModel
    {
        public string Question { get; set; }
        public QuestionType Type { get; set; }

        public IList<CreateAdventureQuestionModel> Children { get; set; }
        
    }
}
