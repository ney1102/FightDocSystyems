using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models.DTOs.Document;
using DocumentEntity = Models.Entity.Document.Document;
namespace Models.DTOs.Fight
{
    public class FlightRequest
    {
        public string FlightNo { get; set; }
        public int DepartureStationID { get; set; }
        public int ArrivalStationID { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set;}
        public IFormFile Document { get; set; }
    }
}
