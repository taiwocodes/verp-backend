using VerpBackendData.Interfaces;
using VerpBackendData.ViewModels.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VerpBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public AuthController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        /// <summary>
        ///  This allows authentication into the application.
        /// </summary>
        /// <param name="authData"></param>
        /// <returns>Authentication Token</returns>
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] AuthenticateUserVM authData)
        {
            var token = _jwtAuthenticationManager.Authenticate(authData);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

    }
}
