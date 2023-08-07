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
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable(nameof(Transaction));

            builder.HasKey(x => x.TransactionId);
            builder.Property(x => x.TransactionId).ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedDate)
                .IsRequired();

            builder.Property(x => x.Value)
                .HasPrecision(13,2)
                .IsRequired();

            builder.Property(x => x.Balance)
                .HasPrecision(13, 2)
                .IsRequired();

            builder.HasOne(c => c.Account)
            .WithMany(p => p.Transactions)
            .HasForeignKey(c => c.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
