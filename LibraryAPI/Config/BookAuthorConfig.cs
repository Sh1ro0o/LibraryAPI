using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Config
{
    public class BookAuthorConfig : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.HasKey(x => new { x.BookId, x.AuthorId });
            builder.Property(x => x.BookId).HasColumnName("BookId").HasColumnType("int");
            builder.Property(x => x.AuthorId).HasColumnName("AuthorId").HasColumnType("int");

            builder
           .HasOne(x => x.Book)
           .WithMany(x => x.BookAuthors)
           .HasForeignKey(x => x.BookId)
           .OnDelete(DeleteBehavior.Cascade);


            builder
            .HasOne(x => x.Author)
            .WithMany(x => x.BookAuthors)
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
