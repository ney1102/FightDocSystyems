﻿using System.Text.Json.Serialization;
using System.Text.Json;
using Core.InterfaceService;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.DTOs.Auth;
using Models.DTOs.Fight;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FightApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IAirportService _airportService;
        private readonly IFlightService _flightService;
        public FlightController(IAirportService airportService, IFlightService flightService)
        {
            _airportService = airportService;
            _flightService = flightService;
        }
        // GET: api/Fight
        [HttpGet]
        public async Task<IActionResult> GetAllFlights()
        {
            try
            {
                var response = await _flightService.GetAllFlightAsync();

                if (response.Succes == true)
                {
                    return Ok(response);
                }
                else
                {
                    return Unauthorized(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex,
                    statusCode = "500"
                });
            }
        }
        // GET: api/Fight/airports
        [HttpGet("airports")]
        public async Task<IActionResult> GetAirports()
        {
            try
            {
                var response = await _airportService.GetAllAirPortAsync();
                if (response.Succes)
                {
                    return Ok(response);
                }
                else return Unauthorized(response);
            }
            catch
            {
                return BadRequest(new
                {
                    message = "500"
                });
            }
        }
        //Get: api/Flight/{id}
        [HttpGet("FlightsByDepartureStation")]
        public async Task<IActionResult> GetFlightsByDepartureStationAsync(int departureStationId)
        {
            var response = await _flightService.GetFlightsByDepartureAsync(departureStationId);
            try
            {
                if (response.Succes)
                    return Ok(response);
                else return Unauthorized(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex,
                    statusCode = "500"
                });
            }
        }
        [HttpGet("FlightsByArrivalStation")]
        public async Task<IActionResult> GetFlightsByArrivalStationAsync(int arrivalStationID)
        {
            var response = await _flightService.GetFlightsByArrivalAsync(arrivalStationID);
            try
            {
                if (response.Succes)
                    return Ok(response);
                else return Unauthorized(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex,
                    statusCode = "500"
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddNewFlight([FromForm] FlightRequest flight)
        {
                var response = await _flightService.AddFlightAsync(flight);
            try
            {
                if (response.Succes==true)
                {
                    return Ok(response);
                }else return Unauthorized(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex,
                    statusCode = "500"
                });
            }
        }
        
    }
}
