using System.Reflection;
using LobsterInk.Application.Interfaces;
using LobsterInk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LobsterInk.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Adventure> Adventures { get; }
        public DbSet<AdventureQuestion> AdventureQuestions { get; }
        public DbSet<UserAdventureQuestionHistory> UserAdventureQuestionsHistory { get; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
