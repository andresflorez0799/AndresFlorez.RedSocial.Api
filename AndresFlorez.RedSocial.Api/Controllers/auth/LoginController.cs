using AndresFlorez.RedSocial.Api.Models;
using AndresFlorez.RedSocial.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AndresFlorez.RedSocial.Api.Controllers.auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : CustomControllerBase
    {
        private IUserAuth _userService;
        public LoginController(IUserAuth userService)
        {
            this._userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Autenticar([FromBody] UserAuthRequest model)
        {
            try
            {
                UserAuthResponse validacionUsuario = _userService.Auth(model);
                if (validacionUsuario != null && !string.IsNullOrEmpty(validacionUsuario.Token))
                {
                    return Ok(GetResponseApi(validacionUsuario, 200, true));
                }
                return Ok(GetResponseApi(false, 404, false));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("validate-token")]
        public IActionResult ValidarToken(string token)
        {
            try
            {
                return Ok(GetResponseApi(_userService.ValidateToken(token), 200, true));
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
        }
    }
}
