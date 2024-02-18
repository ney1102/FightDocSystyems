using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTOs;
using Models.DTOs.Fight;
using Models.Entity.Fight;

namespace Core.InterfaceService
{
    public interface IAirportService
    {
        Task<Response<IEnumerable<Airport>>> GetAllAirPortAsync();
        //Task<Airport> GetAirPortByID(int id);
        //Task<Response<Airport>> AddAirPort(AirportDTO airport);

    }
}
