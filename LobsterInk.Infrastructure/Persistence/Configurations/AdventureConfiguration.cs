using LobsterInk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LobsterInk.Infrastructure.Persistence.Configurations
{
    public class AdventureConfiguration : IEntityTypeConfiguration<Adventure>
    {
        public void Configure(EntityTypeBuilder<Adventure> builder)
        {
            builder.HasKey(adventure => adventure.Id);

            builder.Property(adventure => adventure.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasMany(adventure => adventure.Questions)
                .WithOne(question => question.Adventure)
                .HasForeignKey(question => question.AdventureId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
