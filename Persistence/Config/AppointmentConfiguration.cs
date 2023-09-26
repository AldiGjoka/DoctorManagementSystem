using Domain.Entity;
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
    public class AppointmentConfiguration : IEntityTypeConfiguration<AppointmentAgg>
    {
        public void Configure(EntityTypeBuilder<AppointmentAgg> builder)
        {
            builder.Property(a => a.PatientId).IsRequired();

            builder.Property(a => a.AppointmentDate).IsRequired();

            builder.Property(a => a.StartTime).IsRequired();

            builder.Property(a => a.EndTime).IsRequired();
        }
    }
}
