using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VerpBackendData.ViewModels;
using VerpBackendData.ViewModels.Auth;

namespace VerpBackendData.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        Task<AuthLoginVM> Authenticate(AuthenticateUserVM model);
    }
}
