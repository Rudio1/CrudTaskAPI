using CrudTaskAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ChoreMap : IEntityTypeConfiguration<Chore>
{
    public void Configure(EntityTypeBuilder<Chore> builder)
    {
        builder.ToTable("Chore");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(t => t.Description)
               .HasMaxLength(500);

        builder.HasOne(t => t.Category)
               .WithMany(c => c.Chores)
               .HasForeignKey(t => t.CategoryId);
    }
}
