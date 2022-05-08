using LobsterInk.Domain.Enums;

namespace LobsterInk.Application.Adventures.ViewModels;

public class AdventureQuestionHistoryViewModel
{
    public string Id { get; set; }
    public string Question { get; set; }
    public int Level { get; set; }
    public QuestionType Type { get; set; }
}