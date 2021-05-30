using System;
using System.Collections.Generic;

#nullable disable

namespace AndresFlorez.RedSocial.Modelo.EF
{
    public partial class RsocialPublicacion : Entidad
    {
        public RsocialPublicacion()
        {
            RsocialPublicacionArchivos = new HashSet<RsocialPublicacionArchivo>();
            RsocialPublicacionImagens = new HashSet<RsocialPublicacionImagen>();
            RsocialPublicacionVideos = new HashSet<RsocialPublicacionVideo>();
        }

        public int IdUsuario { get; set; }
        public int IdEstado { get; set; }
        public DateTime Fecha { get; set; }
        public string Texto { get; set; }

        public virtual RsocialPublicacionEstado IdEstadoNavigation { get; set; }
        public virtual RsocialUsuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<RsocialPublicacionArchivo> RsocialPublicacionArchivos { get; set; }
        public virtual ICollection<RsocialPublicacionImagen> RsocialPublicacionImagens { get; set; }
        public virtual ICollection<RsocialPublicacionVideo> RsocialPublicacionVideos { get; set; }
    }
}
