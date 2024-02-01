using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTOs;
using Models.Entity.User;

namespace Core.InterfaceService
{
    public interface AuthenticationService
    {
        Task<Response<User>> LoginAsync(string username, string password);
    }
}
