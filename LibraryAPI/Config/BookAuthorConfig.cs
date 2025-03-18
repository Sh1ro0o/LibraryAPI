using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Config
{
    public class BookAuthorConfig : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.HasKey(x => x.RecordId);
            builder.Property(x => x.RecordId).HasColumnName("Id").HasColumnType("int");
            builder.Property(x => x.BookId).HasColumnName("BookId").HasColumnType("int");
            builder.Property(x => x.AuthorId).HasColumnName("AuthorId").HasColumnType("int");

            builder.HasOne(x => x.Book);
            builder.HasOne(x => x.Author);
        }
    }
}
