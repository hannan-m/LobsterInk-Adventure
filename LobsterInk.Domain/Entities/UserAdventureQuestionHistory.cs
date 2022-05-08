namespace LobsterInk.Domain.Entities;

public class UserAdventureQuestionHistory
{
    public string UserId { get; set; }

    public string AdventureQuestionId { get; set; }
    public virtual AdventureQuestion AdventureQuestion { get; set; }
}