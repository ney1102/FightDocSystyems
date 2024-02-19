using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.InterfaceService;
using Microsoft.EntityFrameworkCore;
using Models.Context;
using Models.DTOs;
using Models.DTOs.Fight;
using Models.Entity.Fight;

namespace Core.ManagerSerice
{
    public class FlightService:IFlightService
    {
        private readonly AppDbContext _appDbContext;
        public FlightService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        //public FlightResponse ConvertToFlightResponse(Flight flight)
        //{
        //    return new FlightResponse
        //    {
        //        FlightNo = flight.FlightNo,
        //        Route = $"{flight.DepartureStation.Code} - {flight.ArrivalStation.Code}",
        //        DepartureDate = flight.DepartureDate
        //    };
        //}
        public async Task<IEnumerable<FlightResponse>> ConvertFlightsToResponses(IEnumerable<Flight> flights)
        {
            return flights.Select(flight => new FlightResponse
            {
                FlightNo = flight.FlightNo,
                Route = $"{flight.DepartureStation.Code} - {flight.ArrivalStation.Code}",
                DepartureDate = flight.DepartureDate
            });
        }
        public async Task<Response<IEnumerable<Flight>>> GetAllFlightAsync()
        {
            var response = new Response<IEnumerable<Flight>>();
            try
            {
                var flights = await _appDbContext.Flight.Where(f => f.Active == 1 && f.Del_flag == 0)
                                                        .Include(f => f.DepartureStation)
                                                        .Include(f => f.ArrivalStation)
                                                        .ToListAsync();
                //var flightResponses = flights.Select(ConvertToFlightResponse).ToList();
                if (flights != null && flights.Any())
                {
                    response.Data = flights;
                    response.StatusCode = 200;
                    response.Message = "Flights retrieved successfully";
                    response.Succes = true;
                }
                else
                {
                    response.StatusCode = 404;
                    response.Message = "No Flights found";
                    response.Succes = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
                response.StatusCode = 500;
                response.Succes = false;
            }
            return response;

        }
    }
}
