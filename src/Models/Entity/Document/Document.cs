using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entity.Fight;

namespace Models.Entity.Document
{
    public class Document : FileEntity
    {
        public int FlightId { get; set; }
        //[ForeignKey("FlightId")]
        //public Flight Filght { get; set; }

    }
}
