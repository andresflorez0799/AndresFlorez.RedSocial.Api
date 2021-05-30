using System;
using System.Collections.Generic;

#nullable disable

namespace AndresFlorez.RedSocial.Modelo.EF
{
    public partial class RsocialPublicacionEstado : Entidad
    {
        public RsocialPublicacionEstado()
        {
            RsocialPublicacions = new HashSet<RsocialPublicacion>();
        }

        public string Nombre { get; set; }

        public virtual ICollection<RsocialPublicacion> RsocialPublicacions { get; set; }
    }
}
