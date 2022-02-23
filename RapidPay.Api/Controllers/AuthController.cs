
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Entities.DTO;
using RapidPay.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService authService;
        public AuthController(IAuthService authService) =>
            this.authService = authService;

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            try
            {
                var user = await authService.Login(login);
                return Ok(user);
            }
            catch(Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
