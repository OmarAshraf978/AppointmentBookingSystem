using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class ServiceProviderConfiguration : IEntityTypeConfiguration<ServiceProvider>
    {
        public void Configure(EntityTypeBuilder<ServiceProvider> builder)
        {
            builder.Property(x => x.BusinessName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(150);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(200);
            builder.HasMany(x => x.Services).WithOne(x => x.ServiceProvider).HasForeignKey(x => x.ServiceProviderId);
            builder.HasIndex(x => x.UserId).IsUnique();
        }
    }
}
