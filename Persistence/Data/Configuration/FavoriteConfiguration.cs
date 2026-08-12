using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Modules;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Configuration
{
    #region FavoriteConfiguration
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Favorite> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired();
            builder.HasIndex(x => new { x.UserId, x.ServiceId }).IsUnique();
            builder.HasOne(x => x.Service)
                   .WithMany(x => x.Favorites)
                   .HasForeignKey(x => x.ServiceId);
        }
    }
    #endregion
}
