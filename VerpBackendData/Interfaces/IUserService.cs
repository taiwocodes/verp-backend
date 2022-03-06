using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VerpBackendData.ViewModels;
using VerpBackendData.ViewModels.User;

namespace VerpBackendData.Interfaces
{
    public interface IUserService
    {
        Task<ResultVM> RegisterUser(RegisterUserVM model);
    }
}
