using LobsterInk.Domain.Enums;

namespace LobsterInk.Application.Adventures.ViewModels
{
    public class AdventureQuestionViewModel
    {
        public string? Id { get; set; }
        public string Question { get; set; }
        public int Level { get; set; }
        public QuestionType Type { get; set; }
        public string? ParentNavigationId { get; set; }
    }
}
