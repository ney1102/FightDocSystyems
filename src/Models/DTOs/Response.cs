using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class Response<TEntity>
    {
        public TEntity Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public bool Succes { get; set; }
    }
}
