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
    public class PublicacionController : CustomControllerBase
    {
        private readonly IPublicacionBl _logica;

        public PublicacionController(IPublicacionBl publicacion)
        {
            _logica = publicacion;
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] PublicacionViewModel publicacion) 
        {
            if (ModelState.IsValid)
            {
                var claimsUser = GetClaimsAuthToken();
                if (claimsUser.Id > 0) /// luego se puede agregar una propiedad para validar si es admin que cree usuario de forma controlada
                {
                    RsocialPublicacion publicacionNueva = new();
                    publicacionNueva.Texto = publicacion.Texto;
                    publicacionNueva.IdUsuario = claimsUser.Id;
                    publicacionNueva.IdEstado = 1;
                    publicacionNueva.Fecha = DateTime.Now;
                    int id = _logica.CrearPublicacion(publicacionNueva);

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
    }
}
