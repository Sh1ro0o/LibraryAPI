using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Config
{
    public class RefreshTokenConfig : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.RecordId);
            builder.Property(x => x.RecordId).HasColumnName("Id").HasColumnType("int");
            builder.Property(x => x.UserId).HasColumnName("UserId").HasColumnType("nvarchar(450)");
            builder.Property(x => x.ExpiresAt).HasColumnName("ExpiresAt").HasColumnType("datetime");
            builder.Property(x => x.CreatedAt).HasColumnName("CreatedAt").HasColumnType("datetime");
            builder.Property(x => x.RevokedAt).HasColumnName("RevokedAt").HasColumnType("datetime").IsRequired(false);

            builder.HasOne(x => x.User);
        }
    }
}
