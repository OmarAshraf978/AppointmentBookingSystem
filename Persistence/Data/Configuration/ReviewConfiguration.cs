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
    #region ReviewConfiguration
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Rating).IsRequired();
            builder.Property(x => x.Comment).IsRequired().HasMaxLength(200);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.ServiceId).IsRequired();
            builder.HasOne(x => x.Service)
                   .WithMany(x => x.Reviews)
                   .HasForeignKey(x => x.ServiceId);
        }
    }
    #endregion
}
