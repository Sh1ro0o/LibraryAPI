using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Config
{
    public class AssistantChatConfig : IEntityTypeConfiguration<AssistantChat>
    {
        public void Configure(EntityTypeBuilder<AssistantChat> builder)
        {
            builder.HasKey(x => x.RecordId);
            builder.Property(x => x.RecordId).HasColumnName("Id").HasColumnType("int");
            builder.Property(x => x.Message).HasColumnName("Message").HasColumnType("nvarchar(500)");
            builder.Property(x => x.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime");
            builder.Property(x => x.SenderType).HasColumnName("SenderType").HasColumnType("nvarchar(50)");
            builder.Property(x => x.UserId).HasColumnName("UserId").HasColumnType("nvarchar(450)");

            builder.HasOne(x => x.User);
        }
    }
}
