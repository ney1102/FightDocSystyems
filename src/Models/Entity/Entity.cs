using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entity
{
    public interface IEntity
    {
        [Key]       
        public int Id { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public int Active { get; set; }
        public int Del_flag { get; set; }
    }
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime Updated_at { get; set; } = DateTime.Now;
        public int Active { get; set; } = 1;
        public int Del_flag { get; set; } = 0;
    }
}
