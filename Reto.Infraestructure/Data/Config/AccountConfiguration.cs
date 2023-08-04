using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Infraestructure.Data.Config
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable(nameof(Account));

            builder.HasKey(x => x.AccountId);

            builder.Property(x => x.AccountId)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.AccountNumber)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.AccountType)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Balance)
                .HasPrecision(13, 2)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.HasOne(c => c.Client)
            .WithMany(p => p.Accounts)
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
