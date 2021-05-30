using AndresFlorez.RedSocial.Api.Models;
using AndresFlorez.RedSocial.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AndresFlorez.RedSocial.Api.Controllers.auth
{
    [Route("api/v1/[controller]")]
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
                    return Ok(GetResponseApi(validacionUsuario, RespuestaHttp.Ok, true));
                }
                return Ok(GetResponseApi(false, RespuestaHttp.Unauthorized, false));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("validate-token")]
        public IActionResult ValidarToken(string token)
        {
            try
            {
                return Ok(GetResponseApi(_userService.ValidateToken(token), RespuestaHttp.Ok, true));
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
        }
    }
}
