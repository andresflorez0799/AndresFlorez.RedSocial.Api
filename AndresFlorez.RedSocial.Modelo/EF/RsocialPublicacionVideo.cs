using System;
using System.Collections.Generic;

#nullable disable

namespace AndresFlorez.RedSocial.Modelo.EF
{
    public partial class RsocialPublicacionVideo : Entidad
    {
        public int IdPublicacion { get; set; }
        public string VideoRuta { get; set; }
        public string VideoNombre { get; set; }
        public string VideoExtension { get; set; }

        public virtual RsocialPublicacion IdPublicacionNavigation { get; set; }
    }
}
