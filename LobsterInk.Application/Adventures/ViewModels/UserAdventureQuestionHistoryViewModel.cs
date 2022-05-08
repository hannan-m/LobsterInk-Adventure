namespace LobsterInk.Application.Adventures.ViewModels;

public class UserAdventureQuestionHistoryViewModel
{
    public string UserId { get; set; }
    public List<AdventureQuestionHistoryViewModel> QuestionHistory { get; set; }
}