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
    public class FightController : ControllerBase
    {
        private readonly IAirportService _airportService;
        private readonly IFlightService _flightService;
        public FightController(IAirportService airportService, IFlightService flightService)
        {
            _airportService = airportService;
            _flightService = flightService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFlights()
        {
            try
            {
                var response = await _flightService.GetAllFlightAsync();
                var flightResponses = await _flightService.ConvertFlightsToResponses(response.Data);
                var successResponse = new Response<IEnumerable<FlightResponse>>
                {
                    Data = flightResponses,
                    Message = response.Message,
                    StatusCode = response.StatusCode,
                    Succes = response.Succes
                };

                if (response.Succes== true)
                {
                    return Ok(successResponse);
                }
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
        
    }
}
