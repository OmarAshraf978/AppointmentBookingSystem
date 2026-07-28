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
    #region WorkingHourConfiguration
    public class WorkingHourConfiguration : IEntityTypeConfiguration<WorkingHour>
    {
        public void Configure(EntityTypeBuilder<WorkingHour> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DayOfWeek).IsRequired();
            builder.Property(x => x.StartTime).IsRequired();
            builder.Property(x => x.EndTime).IsRequired();
            builder.HasOne(x => x.ServiceProvider)
                   .WithMany(x => x.WorkingHours)
                   .HasForeignKey(x => x.ServiceProviderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
    #endregion
}
