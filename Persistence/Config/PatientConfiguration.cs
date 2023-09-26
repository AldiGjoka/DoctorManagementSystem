using Domain.Entity.PatientAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Config
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasMany(p => p.Appointments)
                .WithOne(p => p.Patient);

            builder.HasMany(p => p.Receta)
                .WithOne(p => p.Patient);

            builder.Property(p => p.PersonalNumber)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(p => p.Birthdate)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(p => p.Gender)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(p => p.PhoneNumber)
                .IsRequired()
                .HasMaxLength(256);

            builder.OwnsOne(p => p.Address, address =>
            {
                address.WithOwner();

                address.Property(address => address.State)
                .IsRequired()
                .HasMaxLength(256);

                address.Property(address => address.City)
                .IsRequired()
                .HasMaxLength(256);

                address.Property(address => address.Country)
                .IsRequired()
                .HasMaxLength(256);

                address.Property(address => address.StreetName)
                .IsRequired()
                .HasMaxLength(256);

                address.Property(address => address.PostalCode)
                .IsRequired()
                .HasMaxLength(256);
            });

            builder.Navigation(p => p.Address)
                .IsRequired();
        }
    }
}
