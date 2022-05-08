using System.Text.Json.Serialization;
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

    [JsonIgnore] public string? ParentNavigationId { get; set; }
    [JsonIgnore] public virtual AdventureQuestion? Parent { get; set; }

    public virtual ICollection<AdventureQuestion> Children { get; set; }
    
    public virtual ICollection<UserAdventureQuestionHistory> AdventureQuestionHistories { get; set; }
}