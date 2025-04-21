using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Config
{
    public class BorrowingTransactionConfig : IEntityTypeConfiguration<BorrowingTransaction>
    {
        public void Configure(EntityTypeBuilder<BorrowingTransaction> builder)
        {
            builder.HasKey(x => x.RecordId);
            builder.Property(x => x.RecordId).HasColumnName("Id").HasColumnType("int");
            builder.Property(x => x.BorrowDate).HasColumnName("BorrowDate").HasColumnType("datetime");
            builder.Property(x => x.DueDate).HasColumnName("DueDate").HasColumnType("datetime");
            builder.Property(x => x.ReturnedDate).HasColumnName("ReturnedDate").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.IsReturned).HasColumnName("IsReturned").HasColumnType("bit");
            builder.Property(x => x.CreatedDate).HasColumnName("CreatedDate").HasColumnType("datetime");
            builder.Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.UserId).HasColumnName("UserId").HasColumnType("nvarchar(450)");
            builder.Property(x => x.BookCopyId).HasColumnName("BookCopyId").HasColumnType("int");

            builder.HasOne(x => x.User);
            builder.HasOne(x => x.BookCopy);
        }
    }
}
