using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Metadata;

namespace LibraryAPI.Config
{
    public class BookCopyConfig : IEntityTypeConfiguration<BookCopy>
    {
        public void Configure(EntityTypeBuilder<BookCopy> builder)
        {
            builder.HasKey(x => x.RecordId);
            builder.Property(x => x.RecordId).HasColumnName("Id").HasColumnType("int");
            builder.Property(x => x.SerialNumber).HasColumnName("SerialNumber").HasColumnType("nvarchar(250)");
            builder.Property(x => x.IsAvailable).HasColumnName("IsAvailable").HasColumnType("bit");

            builder.Property(x => x.BookId).HasColumnName("BookId").HasColumnType("int");
            builder.HasOne(x => x.Book);
        }
    }
}
