using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Device.Infrastructure.Mappings
{
    public class DeviceMapping : IEntityTypeConfiguration<Domain.Entities.Device>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Device> builder)
        {
            builder.HasKey(d => d.Id)
              .HasName("DEVICE_PK");

            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(c => c.Name)
                .HasColumnName("ds_name")
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder.Property(c => c.Brand)
               .HasColumnName("ds_brand")
               .HasColumnType("varchar(250)")
               .IsRequired();

            builder.Property(h => h.CreatedDate)
                .HasColumnName("dh_created")
                .IsRequired();

            builder.ToTable("DEVICE");
        }
    }
}
