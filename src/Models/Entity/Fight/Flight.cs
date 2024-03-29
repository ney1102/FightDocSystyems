﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DocumentEntity = Models.Entity.Document.Document;

namespace Models.Entity.Fight
{
    public class Flight:Entity
    {
        public string FlightNo { get; set; }
        public int DepartureStationID { get; set; }
        [ForeignKey("DepartureStationID")]
        [JsonIgnore]
        public Airport DepartureStation { get; set; }
        public DateTime DepartureDate { get; set; }
        public int ArrivalStationID { get; set; }
        [ForeignKey("ArrivalStationID")]
        [JsonIgnore]
        public Airport ArrivalStation { get; set; }
        public DateTime ArrivalDate { get; set;}
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [JsonIgnore]
        public List<DocumentEntity> Documents { get; set; }

    }
}
