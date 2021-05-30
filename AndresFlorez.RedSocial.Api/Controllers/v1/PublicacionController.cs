using AndresFlorez.RedSocial.Api.Models;
using AndresFlorez.RedSocial.Api.Models.ViewModels;
using AndresFlorez.RedSocial.Logica.Contrato;
using AndresFlorez.RedSocial.Modelo.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;

namespace AndresFlorez.RedSocial.Api.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class PublicacionController : CustomControllerBase
    {
        private readonly IPublicacionBl _logica;
        private readonly AppSettings _appSettings;

        public PublicacionController(IPublicacionBl publicacion, IOptions<AppSettings> appSettings)
        {
            _logica = publicacion;
            _appSettings = appSettings.Value;
        }

        [HttpPost("")]
        public IActionResult Post([FromForm] PublicacionViewModel publicacion)
        {
            if (ModelState.IsValid)
            {
                var claimsUser = GetClaimsAuthToken();
                if (claimsUser.Id > 0) //control dummy
                {
                    RsocialPublicacion publicacionNueva = new();
                    publicacionNueva.Texto = publicacion.Texto;
                    publicacionNueva.IdUsuario = claimsUser.Id;
                    publicacionNueva.IdEstado = 1;
                    publicacionNueva.Fecha = DateTime.Now;
                    publicacionNueva.RsocialPublicacionVideos = ObtenerVideos(publicacion.Videos);
                    publicacionNueva.RsocialPublicacionImagens = ObtenerImagenes(publicacion.Imagenes);
                    publicacionNueva.RsocialPublicacionArchivos = ObtenerArchivos(publicacion.Archivos);

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




        #region Helpers
        /// <summary>
        /// Obtiene los archivos cargados por el usuario desde un formulario
        /// </summary>
        /// <param name="archivos"></param>
        /// <returns></returns>
        private ICollection<RsocialPublicacionVideo> ObtenerVideos(IList<IFormFile> archivos) 
        {
            ICollection<RsocialPublicacionVideo> videos = new List<RsocialPublicacionVideo>();           
            if (archivos != null && archivos.Count > 0)
            {
                ValidarDirectorio(_appSettings.DIRFolderVideos);
                foreach (var file in archivos)
                {
                    string fullFilePath = Path.Combine(_appSettings.DIRFolderVideos, file.FileName);
                    if (!System.IO.File.Exists(fullFilePath))
                        using (var stream = System.IO.File.Create(fullFilePath))
                        {
                            file.CopyToAsync(stream);
                        }
                    videos.Add(new RsocialPublicacionVideo()
                    {
                        VideoNombre = Path.GetFileNameWithoutExtension(file.FileName),
                        VideoExtension = Path.GetExtension(file.FileName),
                        VideoRuta = fullFilePath
                    });
                }
            }
            return videos;
        }

        private ICollection<RsocialPublicacionImagen> ObtenerImagenes(IList<IFormFile> archivos)
        {
            ICollection<RsocialPublicacionImagen> videos = new List<RsocialPublicacionImagen>();

            if (archivos != null && archivos.Count > 0)
            {
                ValidarDirectorio(_appSettings.DIRFolderImagenes);
                foreach (var file in archivos)
                {
                    string fullFilePath = Path.Combine(_appSettings.DIRFolderImagenes, file.FileName);
                    if (!System.IO.File.Exists(fullFilePath))
                        using (var stream = System.IO.File.Create(fullFilePath))
                        {
                            file.CopyToAsync(stream);
                        }
                    videos.Add(new RsocialPublicacionImagen()
                    {
                        ImagenNombre = Path.GetFileNameWithoutExtension(file.FileName),
                        ImagenExtension = Path.GetExtension(file.FileName),
                        ImagenRuta = fullFilePath
                    });
                }
            }
            return videos;
        }

        private ICollection<RsocialPublicacionArchivo> ObtenerArchivos(IList<IFormFile> archivos)
        {
            ICollection<RsocialPublicacionArchivo> videos = new List<RsocialPublicacionArchivo>();

            if (archivos != null && archivos.Count > 0)
            {
                ValidarDirectorio(_appSettings.DIRFolderArchivos);
                foreach (var file in archivos)
                {
                    string fullFilePath = Path.Combine(_appSettings.DIRFolderArchivos, file.FileName);
                    if (!System.IO.File.Exists(fullFilePath))
                        using (var stream = System.IO.File.Create(fullFilePath))
                        {
                            file.CopyToAsync(stream);
                        }
                    videos.Add(new RsocialPublicacionArchivo()
                    {
                        ArchivoNombre = Path.GetFileNameWithoutExtension(file.FileName),
                        ArchivoExtension = Path.GetExtension(file.FileName),
                        ArchivoRuta = fullFilePath
                    });
                }
            }
            return videos;
        }

        private void ValidarDirectorio(string directorioRaiz) 
        {
            if (!Directory.Exists(directorioRaiz))
                Directory.CreateDirectory(directorioRaiz);
        }
        #endregion
    }
}
