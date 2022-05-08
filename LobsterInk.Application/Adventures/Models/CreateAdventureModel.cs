namespace LobsterInk.Application.Adventures.Models;

public class CreateAdventureModel
{
    public string Name { get; set; }
    public List<CreateAdventureQuestionModel> Questions { get; set; }
}