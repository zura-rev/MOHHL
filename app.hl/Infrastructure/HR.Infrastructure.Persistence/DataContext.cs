using HR.Core.Application.Interfaces.Contracts;
using HR.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Infrastructure.Persistence
{
    public class DataContext : DbContext
    {

        public DbSet<Employee> Employes { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Structure> Structure { get; set; }
        public DbSet<Vacancy> Vacancy { get; set; }
        private readonly ICurrentUserService user;
        public DataContext(DbContextOptions<DataContext> options, ICurrentUserService user) : base(options) => this.user = user;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
