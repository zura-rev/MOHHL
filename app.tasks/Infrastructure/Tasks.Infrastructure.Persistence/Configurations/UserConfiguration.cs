using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Tasks.Core.Domain.Models;

namespace Tasks.Infrastructure.Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "dbo");

            builder.Property(x => x.Id);
            builder.Property(x => x.UserName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(1000).IsRequired();

            builder.Property(x => x.PrivateNumber).HasMaxLength(11).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.LastName).HasMaxLength(50);

            builder.Property(x => x.Description).HasMaxLength(1000);

            #region აღწერილია: უნიკალური და არაკლასტერიზებული ინდექსები
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.HasIndex(x => x.PrivateNumber).IsUnique();
            #endregion

            builder.HasData(new
            {
                Id = 1,
                UserName = "admin",
                Password = "135991F75307D7442CEB1F1A39504AB0", // 122333
                PrivateNumber = "00000000001",
                FirstName = "ზურაბ",
                LastName = "რევაზიშვილი",
                DateCreated = DateTime.Now,
                Description = "ადმინისტრატორი",
                UserId = 0
            });

            builder.HasData(new
            {
                Id = 2,
                UserName = "super",
                Password = "90EA53D8F91C21B9A364DBAD988C4C98", // 123456
                PrivateNumber = "00000000002",
                FirstName = "მარიკა",
                LastName = "ზარნაძე",
                DateCreated = DateTime.Now,
                Description = "სუპერვაიზერი",
                UserId = 0
            });

            builder.HasData(new
            {
                Id = 3,
                UserName = "oper",
                Password = "E04CF44BD1C6B27DE5C79FB2012A01F2", // 123456
                PrivateNumber = "00000000003",
                FirstName = "ნინო",
                LastName = "მოდებაძე",
                DateCreated = DateTime.Now,
                Description = "ოპერატორი",
                UserId = 0
            });

            builder.HasQueryFilter(x => !x.DateDeleted.HasValue);
        }
    }
}
