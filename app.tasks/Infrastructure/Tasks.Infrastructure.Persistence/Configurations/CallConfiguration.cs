using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Tasks.Core.Domain.Models;
using Tasks.Core.Domain.Enums;

namespace Tasks.Infrastructure.Persistence.Configurations
{
    internal class CallConfiguration : IEntityTypeConfiguration<Call>
    {
        public void Configure(EntityTypeBuilder<Call> builder)
        {
            builder.ToTable("Calls", "dbo");
            //builder.Ignore(x => x.Applications);
            builder.Property(x => x.Id);
            builder.Property(x => x.CallAuthor).HasMaxLength(20).IsRequired();
            builder.Property(x => x.PrivateNumber).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Note).HasMaxLength(50).IsRequired();
            builder.Property(x => x.CreateDate);
            builder.Property(x => x.CallType).HasMaxLength(100);


            #region აღწერილია: საკუთრებაში მყოფი (Owned) ტიპები
            //builder.OwnsOne(x => x.PhoneConfirm).Property(x => x.Code).HasMaxLength(10);
            #endregion

            #region აღწერილია: უნიკალური და არაკლასტერიზებული ინდექსები
            //builder.HasIndex(x => x.PrivateNumber).IsUnique();
            #endregion

            #region აღწერილია: მასივის (Array) ტიპის ველები
            //builder.Property(x => x.PreviousFirstNames)
            //    .HasMaxLength(500)
            //    .HasConversion(
            //        v => string.Join(';', v),
            //        v => v.Split(';', StringSplitOptions.RemoveEmptyEntries));

            //builder.Property(x => x.PreviousLastNames)
            //    .HasMaxLength(500)
            //    .HasConversion(
            //        v => string.Join(';', v),
            //        v => v.Split(';', StringSplitOptions.RemoveEmptyEntries));

            //builder.Property(x => x.ChronicDiseases)
            //    .HasMaxLength(1000)
            //    .HasConversion(
            //        v => string.Join(';', v),
            //        v => v.Split(';', StringSplitOptions.RemoveEmptyEntries));
            #endregion

            #region აღწერილია: ფუნქციონალი კლასში, ორი ერთი და იგივე ტიპის property-ის არსებობისთვის
            //builder.HasOne(d => d.Performers)
            //    .WithOne()
            //    .HasForeignKey<Call>("OperId");

            //builder.HasOne(d => d.Supervaiser)
            //   .WithOne()
            //   .HasForeignKey<Call>("SupervaiserId");
            //.HasConstraintName("FK(SupervaiserId->User.Id)");
            #endregion

            //builder.HasQueryFilter(x => !x.DateDeleted.HasValue && x.DateConfirmed.HasValue);
            //builder.HasOne(x => x.Card)
            //    .WithOne(x => x.Call)
            //    .HasConstraintName("CallId");
        }
    }
}
