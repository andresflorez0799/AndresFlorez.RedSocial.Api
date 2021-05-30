using System;
using System.Collections.Generic;

#nullable disable

namespace AndresFlorez.RedSocial.Modelo.EF
{
    public partial class RsocialMensajeriaEstado : Entidad
    {
        public RsocialMensajeriaEstado()
        {
            RsocialMensajeria = new HashSet<RsocialMensajerium>();
        }

        public string Nombre { get; set; }

        public virtual ICollection<RsocialMensajerium> RsocialMensajeria { get; set; }
    }
}
