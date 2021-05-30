using System;
using System.Collections.Generic;

#nullable disable

namespace AndresFlorez.RedSocial.Modelo.EF
{
    public partial class RsocialPublicacionImagen : Entidad
    {
        public int IdPublicacion { get; set; }
        public string ImagenRuta { get; set; }
        public string ImagenNombre { get; set; }
        public string ImagenExtension { get; set; }

        public virtual RsocialPublicacion IdPublicacionNavigation { get; set; }
    }
}
