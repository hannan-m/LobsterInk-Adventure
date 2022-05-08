using LobsterInk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LobsterInk.Infrastructure.Persistence.Configurations;

public class UserAdventureQuestionHistoryConfiguration : IEntityTypeConfiguration<UserAdventureQuestionHistory>
{
    public void Configure(EntityTypeBuilder<UserAdventureQuestionHistory> builder)
    {
        builder.HasKey(adventure => new { adventure.UserId, adventure.AdventureQuestionId });

        builder.HasOne(adventure => adventure.AdventureQuestion)
            .WithMany(question => question.AdventureQuestionHistories)
            .HasForeignKey(question => question.AdventureQuestionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}