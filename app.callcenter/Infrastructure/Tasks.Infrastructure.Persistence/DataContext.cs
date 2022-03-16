using Tasks.Core.Application.Interfaces.Contracts;
using Tasks.Core.Domain.Models;
using Tasks.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Core.Domain.Common;
using System.Collections.Generic;

namespace Tasks.Infrastructure.Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<Call> Calls { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; private set; }
        public DbSet<Resource> Resources { get; private set; }


        private readonly ICurrentUserService user;
        public DataContext(DbContextOptions<DataContext> options, ICurrentUserService user) : base(options) => this.user = user;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new ResourceConfiguration());
            builder.ApplyConfiguration(new CallConfiguration());
            builder.ApplyConfiguration(new CardConfiguration());

            //builder.Entity<User>()
            //    .HasMany(r => r.Resources)
            //    .WithMany(u => u.Users)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "ResourceUser",
            //        r => r.HasOne<Resource>().WithMany().HasForeignKey("ResourceId"),
            //        u => u.HasOne<User>().WithMany().HasForeignKey("UserId"),
            //        je =>
            //        {
            //            je.HasKey("UserId", "ResourceId");
            //            je.HasData(
            //                new { UserId = 1, ResourceId = 1 });
            //        });
        }

        #region SaveChanges -ების გადატვირთვა
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
                this.Audition(entry);

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
                this.Audition(entry);
            return base.SaveChanges();
        }

        private void Audition(EntityEntry<AuditableEntity> entry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    //entry.Entity.DateCreated = DateTime.Now;
                    entry.Entity.UserId = user.AccountId;
                    break;
                case EntityState.Modified:
                    // არ შეიცვლება ქვემოთ ჩამოთვლილი ველები
                    //entry.Property(nameof(AuditableEntity.CreatedBy)).IsModified = false;
                    //entry.Property(nameof(AuditableEntity.DateCreated)).IsModified = false;
                    //entry.Property(nameof(AuditableEntity.DeletedBy)).IsModified = false;
                    //entry.Property(nameof(AuditableEntity.DateDeleted)).IsModified = false;
                    entry.Entity.UserId = user.AccountId;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged;
                    // შეიცვლება მხოლოდ ქვემოთ ჩამოთვლილი ველები
                    entry.Entity.DateDeleted = DateTime.Now;
                    //entry.Entity.DeletedBy = user.AccountId;
                    break;
            };
        }


        #endregion
    }
}
