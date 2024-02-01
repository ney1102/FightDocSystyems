using Core.InterfaceService;
using Microsoft.EntityFrameworkCore;
using Models.Context;

namespace Core.ManagerSerice
{
    public class AuthencaitionManager: AuthenticationService
    {
        private readonly AppDbContext _appDbContext;
        public AuthencaitionManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await _appDbContext.User.FirstOrDefaultAsync(u=>u.Email==email&&u.Password==password);
            return user != null;
        }
    }
}
