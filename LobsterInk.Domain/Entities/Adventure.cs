namespace LobsterInk.Domain.Entities
{
    public class Adventure
    {
        public string? Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AdventureQuestion> Questions { get; set; }
    }
}
