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
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable(nameof(Client));

            builder.HasKey(x => x.ClientId);

            builder.Property(x => x.ClientId).ValueGeneratedOnAdd();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Password)
                .IsRequired();

            builder.HasOne(c => c.Person)
            .WithMany(p => p.Clients) 
            .HasForeignKey(c => c.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
