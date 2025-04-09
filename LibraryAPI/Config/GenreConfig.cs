using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Config
{
    public class GenreConfig : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(x => x.RecordId);
            builder.Property(x => x.RecordId).HasColumnName("Id").HasColumnType("int");
            builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(50)");
            builder.Property(x => x.Description).HasColumnName("Description").HasColumnType("nvarchar(500)").IsRequired(false);

            builder.HasMany(x => x.BookGenres);
        }
    }
}
