using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentEntity = Models.Entity.Document.Document;

namespace Models.DTOs.Fight
{
    public class FlightResponse
    {
        public string FlightNo { get; set; }
        public string Route { get; set; }
        public DateTime DepartureDate { get; set; }
        public ICollection<DocumentEntity> Documents { get; set; }
    }
}
