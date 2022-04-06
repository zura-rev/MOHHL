using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using Hl.Core.Domain.Models;

namespace Hl.Infrastructure.Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "dbo");

            builder.Property(x => x.Id);
            builder.Property(x => x.UserName).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(1000).IsRequired();

            builder.Property(x => x.PrivateNumber).HasMaxLength(11).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(250);
            builder.Property(x => x.LastName).HasMaxLength(250);

            builder.Property(x => x.Description);

            #region აღწერილია: უნიკალური და არაკლასტერიზებული ინდექსები
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.HasIndex(x => x.PrivateNumber).IsUnique();
            #endregion

            builder.HasData(new User
            {
                Id=1,
                UserName = "admin",
                Password = "2D08086927F4D87A31154AAF0BA2E067", // 123456
                PrivateNumber = "00000000001",
                FirstName = "ზურაბ",
                LastName = "რევაზიშვილი",
                Description = "ადმინისტრატორი",
                IsActive=true,
                ResetPassword=false
                //Resources = new List<Resource>
                //{
                //    new Resource { Name = "ROLE.ADMIN", Description = "როლი ადმინისტრატორი" }
                //}
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
                IsActive = true,
                ResetPassword = false
                //Resources = new List<Resource>
                //{
                //    new Resource{
                //        Id=2,
                //        Name="ROLE.SUPERVAISER",
                //        Description="როლი სუპერვაიზერი"
                //    }
                //}
            });

            builder.HasData(new
            {
                Id = 3,
                UserName = "oper",
                Password = "604DB3EA93CB81194B8D47905ED15DD2", // 123456
                PrivateNumber = "00000000003",
                FirstName = "ნინო",
                LastName = "მოდებაძე",
                DateCreated = DateTime.Now,
                Description = "ოპერატორი",
                IsActive = true,
                ResetPassword = false
                //Resources = new List<Resource> {
                //    new Resource{
                //        Id = 3,
                //        Name = "ROLE.OPERATOR",
                //        Description = "როლი ოპერატორი"
                //    }
                //}
            });

            //builder.HasData(new
            //{
            //    Id = 4,
            //    UserName = "super2",
            //    Password = "0F720A1B7C5038FB60CFC626E519C02B", // 123456
            //    PrivateNumber = "00000000004",
            //    FirstName = "ნია",
            //    LastName = "მაირურაძე",
            //    DateCreated = DateTime.Now,
            //    Description = "სუპერვაიზერი",
            //    Resources = new List<Resource> {
            //        new Resource{
            //            Id = 2,
            //            Name = "ROLE.SUPERVAISER",
            //            Description = "როლი სუპერვაიზერი"
            //        }
            //    }
            //});

            //builder.HasQueryFilter(x => !x.DateDeleted.HasValue);

        }
    }
}
