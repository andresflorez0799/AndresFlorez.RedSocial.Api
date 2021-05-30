using System;
using System.Collections.Generic;

#nullable disable

namespace AndresFlorez.RedSocial.Modelo.EF
{
    public partial class RsocialPublicacionArchivo : Entidad
    {
        public int IdPublicacion { get; set; }
        public string ArchivoRuta { get; set; }
        public string ArchivoNombre { get; set; }
        public string ArchivoExtension { get; set; }

        public virtual RsocialPublicacion IdPublicacionNavigation { get; set; }
    }
}
