using LobsterInk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LobsterInk.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Adventure> Adventures { get; }
        DbSet<AdventureQuestion> AdventureQuestions { get; }
        DbSet<UserAdventureQuestionHistory> UserAdventureQuestionsHistory { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
