using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entity.Fight
{
    public class Airport:Entity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Created_by { get; set; }
        public int? Updated_by { get; set; }
    }
}
