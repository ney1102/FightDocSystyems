using Core.InterfaceService;
using Microsoft.EntityFrameworkCore;
using Models.Context;
using Models.DTOs;
using Models.Entity.User;

namespace Core.ManagerSerice
{
    public class AuthencaitionManager: AuthenticationService
    {
        private readonly AppDbContext _appDbContext;
        public AuthencaitionManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Response<User>> LoginAsync(string email, string password)
        {
            var user = await _appDbContext.User.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            var userResponse = new Response<User>();
            if (user != null)
            {
                userResponse.Data = user;
                userResponse.StatusCode = 200;
                userResponse.Message = "Login Succes";
                userResponse.Succes = true;
                return userResponse;
            }
            else
            {
                userResponse.Message = "Invalid email or password!";
                userResponse.StatusCode = 401; 
                userResponse.Succes = false;
            }
            return userResponse;
        }
    }
}
