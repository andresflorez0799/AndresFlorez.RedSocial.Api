using System;

namespace AndresFlorez.RedSocial.Api.Models.ViewModels
{
    public class PublicacionViewModel
    {
        public int IdUsuario { get; set; }
        public int IdEstado { get; set; }
        public DateTime Fecha { get; set; }
        public string Texto { get; set; }
    }
}
