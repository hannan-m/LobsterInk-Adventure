using LobsterInk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LobsterInk.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Adventure> Adventures { get; set; }
        DbSet<AdventureQuestion> AdventureQuestions { get; set; }
        DbSet<UserAdventureQuestionHistory> UserAdventureQuestionsHistory { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
