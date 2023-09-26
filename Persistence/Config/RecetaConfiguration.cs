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
    public class RecetaConfiguration : IEntityTypeConfiguration<Receta>
    {
        public void Configure(EntityTypeBuilder<Receta> builder)
        {
            builder.Property(r => r.PatientId).IsRequired();

            builder.Property(r => r.Date).IsRequired();

            builder.OwnsMany(r => r.Prescriptions, receta =>
            {
                receta.WithOwner();

                receta.Property(receta => receta.MedicationName)
                .IsRequired()
                .HasMaxLength(256);

                receta.Property(receta => receta.Dosage)
                .IsRequired()
                .HasMaxLength(256);

                receta.Property(receta => receta.Instructions)
                .IsRequired()
                .HasMaxLength(256);
            });
        }
    }
}
