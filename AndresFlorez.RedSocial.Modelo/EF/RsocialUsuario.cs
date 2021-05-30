using System;
using System.Collections.Generic;

#nullable disable

namespace AndresFlorez.RedSocial.Modelo.EF
{
    public partial class RsocialUsuario : Entidad
    {
        public RsocialUsuario()
        {
            RsocialMensajeriumIdUsuarioDestinoNavigations = new HashSet<RsocialMensajerium>();
            RsocialMensajeriumIdUsuarioOrigenNavigations = new HashSet<RsocialMensajerium>();
            RsocialPublicacions = new HashSet<RsocialPublicacion>();
        }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public bool IsBloqueado { get; set; }
        public bool? IsActivo { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<RsocialMensajerium> RsocialMensajeriumIdUsuarioDestinoNavigations { get; set; }
        public virtual ICollection<RsocialMensajerium> RsocialMensajeriumIdUsuarioOrigenNavigations { get; set; }
        public virtual ICollection<RsocialPublicacion> RsocialPublicacions { get; set; }
    }
}
