using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Config
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasMany(d => d.Patients)
                .WithOne(d => d.Doctor);

            builder.Property(d => d.PersonalNumber)
                .IsRequired();

            builder.Property(d => d.FirstName)
                .IsRequired();

            builder.Property(d => d.LastName)
                .IsRequired();

            builder.Property(d => d.Specialization)
                .IsRequired();

            builder.Property(d => d.Email)
                .IsRequired();

            builder.Property(d => d.PhoneNumber)
                .IsRequired();

            builder.OwnsOne(d => d.Address, address =>
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

            builder.Navigation(d => d.Address)
                .IsRequired();
        }
    }
}
