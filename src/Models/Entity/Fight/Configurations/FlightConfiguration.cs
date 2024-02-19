using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.Entity.Fight.Configurations
{
    public class FlightConfiguration:IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.HasOne(f => f.DepartureStation)
            .WithMany(a => a.FlightsDeparture)
            .HasForeignKey(f => f.DepartureStationID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(true);

            builder.HasOne(f => f.ArrivalStation)
                .WithMany(a => a.FlightsArrival)
                .HasForeignKey(f => f.ArrivalStationID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);
        }
    }
}
