using System;
using System.Collections.Generic;

#nullable disable

namespace AndresFlorez.RedSocial.Modelo.EF
{
    public partial class RsocialMensajerium : Entidad
    {
        public int IdUsuarioOrigen { get; set; }
        public int IdUsuarioDestino { get; set; }
        public int IdEstado { get; set; }
        public DateTime Fecha { get; set; }
        public string Mensaje { get; set; }

        public virtual RsocialMensajeriaEstado IdEstadoNavigation { get; set; }
        public virtual RsocialUsuario IdUsuarioDestinoNavigation { get; set; }
        public virtual RsocialUsuario IdUsuarioOrigenNavigation { get; set; }
    }
}
