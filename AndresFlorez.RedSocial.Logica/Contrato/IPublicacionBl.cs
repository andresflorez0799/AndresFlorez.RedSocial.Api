using AndresFlorez.RedSocial.Modelo.EF;
using System.Collections.Generic;

namespace AndresFlorez.RedSocial.Logica.Contrato
{
    public interface IPublicacionBl
    {
        int CrearPublicacion(RsocialPublicacion publicacion);

        IEnumerable<RsocialPublicacion> ConsultarPublicacion();
    }
}
