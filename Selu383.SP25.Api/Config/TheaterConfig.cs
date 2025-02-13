using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Selu383.SP25.Api.Entities;

namespace Selu383.SP25.Api.Config
{
    public class TheaterConfig : IEntityTypeConfiguration<Theater>
    {
        public void Configure(EntityTypeBuilder<Theater> builder)
        {
            builder.HasKey(t => t.Id); // Primary key
            builder.Property(t => t.Name).IsRequired().HasMaxLength(120);
            builder.Property(t => t.SeatCount).IsRequired();
            builder.Property(t => t.Address).IsRequired();

            builder.ToTable(t => t.HasCheckConstraint("CK_Theater_SeatCount_MinValue", "SeatCount >= 1"));
        }
    }
}

