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
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable(nameof(Person));

            builder.HasKey(x => x.PersonId);

            builder.Property(x => x.PersonId).ValueGeneratedOnAdd();

            builder.Property(x => x.Address)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.Age)
                .IsRequired();

            builder.Property(x => x.Gender)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.Identification)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}
