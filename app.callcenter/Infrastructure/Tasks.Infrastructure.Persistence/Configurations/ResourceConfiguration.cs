using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Tasks.Core.Domain.Models;

namespace Tasks.Infrastructure.Persistence.Configurations
{
    internal class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable("Resources", "dbo");

            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(100);

            builder.HasData(
                 new Resource { Id = 1, Name = "ROLE.ADMIN", Description = "როლი ადმინისტრატორი" },
                 new Resource { Id = 2, Name = "ROLE.SUPERVAISER", Description = "როლი სუპერვაიზერი" },
                 new Resource { Id = 3, Name = "ROLE.OPERATOR", Description = "როლი ოპერატორი" }
            );

        }
    }
}
