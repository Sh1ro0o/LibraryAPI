using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Config
{
    public class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.RecordId);
            builder.Property(x => x.RecordId).HasColumnName("Id").HasColumnType("int");
            builder.Property(x => x.FirstName).HasColumnName("FirstName").HasColumnType("nvarchar(250)").IsRequired(false);
            builder.Property(x => x.LastName).HasColumnName("LastName").HasColumnType("nvarchar(250)").IsRequired(false);
        }
    }
}
