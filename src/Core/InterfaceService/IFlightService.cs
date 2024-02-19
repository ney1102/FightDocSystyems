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
    public interface IFlightService
    {
        Task<Response<IEnumerable<Flight>>> GetAllFlightAsync();
        // Trong IFlightService
        Task<IEnumerable<FlightResponse>> ConvertFlightsToResponses(IEnumerable<Flight> flights);
        Task<Response<Flight>> AddFlightAsync(FlightRequest flight);

    }
}
