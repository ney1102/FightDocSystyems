using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Entity.Fight
{
    public class Airport:Entity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Created_by { get; set; }
        public int? Updated_by { get; set; }
        [JsonIgnore]
        [InverseProperty("DepartureStation")]
        public ICollection<Flight> FlightsDeparture { get; set; }
        [JsonIgnore]
        [InverseProperty("ArrivalStation")]
        public ICollection<Flight> FlightsArrival { get; set; }
    }
}
