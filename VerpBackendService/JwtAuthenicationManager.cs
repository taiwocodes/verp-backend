using VerpBackendData.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using VerpBackendData.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using VerpBackendData;
using System.Threading.Tasks;
using VerpBackendData.ViewModels;
using VerpBackendData.ViewModels.Auth;

namespace VerpBackendService
{
    public class JwtAuthenicationManager : IJwtAuthenticationManager
    {
        private readonly VerpBackendDataContext _context;
        private readonly IConfiguration _configuration;

        public JwtAuthenicationManager(VerpBackendDataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthLoginVM> Authenticate(AuthenticateUserVM model)
        {
            var result = new AuthLoginVM();
            result.ErrorMessages = new List<string>();

            try
            {
                User user = await _context.Users
                    .FirstOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber && x.Password == model.Password);
                if (user == null)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessages.Add("Invalid Credentials, Kindly input your correct email and password");
                    return result;
                }

                string tokenString = GenerateJSONAuthApiToken(user);
                user.AuthenticationToken = tokenString;
                user.LastLoginDate = DateTime.Now;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                result.Token = tokenString;
                result.IsSuccessful = true;
                result.Message = "Login Successful";
                result.UserData = user;
            }
            catch (Exception error)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(error.Message);
                //result.ErrorMessages.Add(error.StackTrace);
            }

            return result;
        }

        private string GenerateJSONWebToken(User loggedUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, loggedUser.PhoneNumber),
                new Claim(JwtRegisteredClaimNames.Email, loggedUser.PhoneNumber),
                new Claim("Id", loggedUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateJSONAuthApiToken(User loggedUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.Name, loggedUser.PhoneNumber)
                        }
                    ),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
