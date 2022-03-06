using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VerpBackendData;
using VerpBackendData.Interfaces;
using VerpBackendData.ViewModels;
using VerpBackendData.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using VerpBackendData.Models;

namespace VerpBackendService
{
    public class UserService : IUserService
    {
        private readonly VerpBackendDataContext _context;
        private readonly IConfiguration _configuration;

        public UserService(VerpBackendDataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ResultVM> RegisterUser(RegisterUserVM model)
        {
            var result = new AuthLoginVM();
            result.ErrorMessages = new List<string>();

            try
            {
                if(await _context.Users.AnyAsync(x => x.PhoneNumber == model.PhoneNumber || x.Email == model.Email))
                {
                    result.ErrorMessages.Add("User with this email or phone number already exists");
                    return result;
                }

                User newUser = new User()
                {
                    //LastName
                };
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                await SendUserTokenViaEmail(model.Email);
                result.IsSuccessful = true;
                result.Message = "Kindly check your email for your token";
            }
            catch(Exception error)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(error.Message);
            }

            return result;
        }

        private async Task<bool> SendUserTokenViaEmail(string email)
        {
            return true;
        }
    }
}
