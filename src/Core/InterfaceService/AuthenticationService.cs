using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InterfaceService
{
    public interface AuthenticationService
    {
        Task<bool> LoginAsync(string username, string password);
    }
}
