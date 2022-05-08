using LobsterInk.Domain.Enums;

namespace LobsterInk.Domain.Entities;

public class AdventureQuestion
{
    public string? Id { get; set; }
    public string Question { get; set; }
    public int Level { get; set; }
    public QuestionType Type { get; set; }

    public string? AdventureId { get; set; }
    public virtual Adventure? Adventure { get; set; }

    public string? ParentNavigationId { get; set; }
    public virtual AdventureQuestion? Parent { get; set; }

    public ICollection<AdventureQuestion> Children { get; set; }
    public ICollection<UserAdventureQuestionHistory> AdventureQuestionHistories { get; set; }
}