using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace AndresFlorez.RedSocial.Api.Models.ViewModels
{
    public class PublicacionViewModel
    {
        public int IdUsuario { get; set; }
        public int IdEstado { get; set; }
        public DateTime Fecha { get; set; }
        public string Texto { get; set; }
        public IList<IFormFile> Videos { get; set; }
        public IList<IFormFile> Archivos { get; set; } 
        public IList<IFormFile> Imagenes { get; set; }
    }
}
