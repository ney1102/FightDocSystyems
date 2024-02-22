﻿using System;
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
        private async Task<FlightResponse> ConvertToFlightResponse(string flightNo)
        {
            var result = await  _appDbContext.Flight
                                             .Where(f => f.Active == 1 && f.Del_flag == 0 && f.FlightNo == flightNo)
                                             .Include(f => f.DepartureStation)
                                             .Include(f => f.ArrivalStation)
                                             .FirstOrDefaultAsync();
            if (result != null)
            {
                return new FlightResponse
                {
                    FlightNo = result.FlightNo,
                    Route = $"{result.DepartureStation.Code} - {result.ArrivalStation.Code}",
                    DepartureDate = result.DepartureDate
                };
            }

            return null;
        }
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
        private async Task<Flight> CreateNewFlightAsync(FlightRequest flightRequest)
        {
            var newFlight = new Flight()
            {
                FlightNo = flightRequest.FlightNo,
                DepartureStationID = flightRequest.DepartureStationID,
                ArrivalStationID = flightRequest.ArrivalStationID,
                DepartureDate = flightRequest.DepartureDate,
                CreatedBy = 5,
                UpdatedBy = 5,
            };

            _appDbContext.Flight.Add(newFlight);
            await _appDbContext.SaveChangesAsync();

            return newFlight;
        }
        public async Task<Response<FlightResponse>> AddFlightAsync(FlightRequest flight)
        {
            var response = new Response<FlightResponse>();
            try
            {
                bool isFlightExist = await _appDbContext.Flight.AnyAsync(f => f.FlightNo == flight.FlightNo);

                if (isFlightExist)
                {
                    response.Message = "Flight with the same FlightNo already exists";
                    response.StatusCode = 401;
                    response.Succes = false;
                    return response;
                }
                var newFlight = await CreateNewFlightAsync(flight);
                var data = await ConvertToFlightResponse(newFlight.FlightNo);
                if (data != null)
                {
                    response.Data = data;
                }
                    response.StatusCode = 201;
                    response.Message = "Added new flight successfully";
                    response.Succes = true;
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
