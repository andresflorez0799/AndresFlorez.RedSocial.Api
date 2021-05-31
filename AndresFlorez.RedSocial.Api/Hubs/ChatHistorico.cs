using System;

namespace AndresFlorez.RedSocial.Api.Hubs
{
    public class ChatHistorico
    {
        public DateTime Fecha { get; set; }
        public int IdUsuarioOrigen { get; set; }
        public int IdUsuarioDestino { get; set; }
        public string Mensaje { get; set; }
    }
}
