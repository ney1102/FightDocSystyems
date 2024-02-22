using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Models.DTOs.Document
{
    public class DocumentRequest
    {
        public IFormFile DocumentDetail { get; set; }
        public int DocumentTypeId { get; set; }
        public int FlightId { get; set; }
    }
}
