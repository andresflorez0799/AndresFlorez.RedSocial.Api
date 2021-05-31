using AndresFlorez.RedSocial.Modelo.EF;
using System.Collections.Generic;

namespace AndresFlorez.RedSocial.Logica.Contrato
{
    public interface IComunicacionBl
    {
        void GuardarMensaje(RsocialMensajerium mensaje);
        IEnumerable<RsocialMensajerium> ConsultarMensajes(int idUsuario);
    }
}
