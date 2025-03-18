using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.RecordId);
            builder.Property(x => x.RecordId).HasColumnName("Id").HasColumnType("int");
            builder.Property(x => x.Title).HasColumnName("Title").HasColumnType("nvarchar(250)");
            builder.Property(x => x.PublishDate).HasColumnName("PublishDate").HasColumnType("date").IsRequired(false);
            builder.Property(x => x.ISBN).HasColumnName("ISBN").HasColumnType("nvarchar(250)").IsRequired(false);
            builder.Property(x => x.Description).HasColumnName("Description").HasColumnType("nvarchar(2000)").IsRequired(false);
        }
    }
}
