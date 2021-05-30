using AndresFlorez.RedSocial.Api.Models.ViewModels;
using AndresFlorez.RedSocial.Logica.Contrato;
using AndresFlorez.RedSocial.Modelo.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AndresFlorez.RedSocial.Api.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : CustomControllerBase
    {
        private readonly IUsuarioBl _usuarioBLL;

        public UsuarioController(IUsuarioBl usuarioBl)
        {
            _usuarioBLL = usuarioBl;
        }


        [HttpPost("")]
        [AllowAnonymous]
        public IActionResult Post([FromBody] UsuarioViewModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var claimsUser = GetClaimsAuthToken();
                    if (claimsUser.Id > 0) /// luego se puede agregar una propiedad para validar si es admin que cree usuario de forma controlada
                    {
                        RsocialUsuario usuarioNuevo = new();
                        usuarioNuevo.Nombre = usuario.Nombre;
                        usuarioNuevo.Apellido = usuario.Apellido;
                        usuarioNuevo.Telefono = usuario.Telefono;
                        usuarioNuevo.Email = usuario.Email;
                        usuarioNuevo.Contrasena = Utilidades.Crypto.EncriptarAES(usuario.Contrasena);
                        int id = _usuarioBLL.CrearUsuario(usuarioNuevo);

                        if (id <= 0)
                            return Ok(GetResponseApi(false, RespuestaHttp.BadRequest, false));
                        return Ok(GetResponseApi(id, RespuestaHttp.Ok, true));
                    }
                    else
                        return Ok(GetResponseApi(false, RespuestaHttp.Unauthorized, false));
                }
                else
                    return BadRequest("Modelo de petición requerido");
            }
            catch (Exception)
            {
                //string.Format("{0}.{1}", this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name)
                return BadRequest("Error en la solicitud");
            }
        }


    }
}
