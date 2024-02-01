using System.IdentityModel.Tokens.Jwt;
using Core.InterfaceService;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FDSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly AuthenticationService _authenService;
        public IConfiguration _configuration;
        public AuthenController(AuthenticationService authenService, IConfiguration configuration)
        {
            _authenService = authenService;
            _configuration = configuration;
        }

        // GET: api/<AuthenController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthenController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthenController>
        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LoginDTO login)
        {
            try
            {
                var loginResponse = await _authenService.LoginAsync(login.Email, login.Password);
                if (loginResponse.Succes)
                {
                    return Ok(loginResponse);
                }
                else return Unauthorized(loginResponse);
            }
            catch
            {
                return BadRequest(new
                {
                    message = "404"
                });
            }
        }

        // PUT api/<AuthenController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthenController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
