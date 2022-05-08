using LobsterInk.Domain.Entities;
using LobsterInk.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LobsterInk.Infrastructure.Persistence.Configurations;

public class AdventureQuestionConfiguration : IEntityTypeConfiguration<AdventureQuestion>
{
    public void Configure(EntityTypeBuilder<AdventureQuestion> builder)
    {
        builder.HasKey(question => question.Id);

        builder.Property(question => question.Question)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(question => question.Type)
            .HasConversion(
                v => v.ToString(),
                v => (QuestionType)Enum.Parse(typeof(QuestionType), v));

        builder.HasOne(question => question.Parent)
            .WithMany(question => question.Children)
            .HasForeignKey(question => question.ParentNavigationId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}