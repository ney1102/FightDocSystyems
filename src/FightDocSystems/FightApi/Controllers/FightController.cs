using Core.InterfaceService;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FightApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FightController : ControllerBase
    {
        private readonly IAirportService _airportService;
        public FightController(IAirportService airportService)
        {
            _airportService = airportService;
        }
        [HttpGet("get-airports")]
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
                    message = "404"
                });
            }
        }
        
    }
}
