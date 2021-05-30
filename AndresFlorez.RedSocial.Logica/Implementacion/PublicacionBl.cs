using AndresFlorez.RedSocial.Datos;
using AndresFlorez.RedSocial.Logica.Contrato;
using AndresFlorez.RedSocial.Modelo.EF;

namespace AndresFlorez.RedSocial.Logica.Implementacion
{
    public class PublicacionBl : IPublicacionBl
    {
        private readonly IRepositorio<RsocialPublicacion> _repositorio;

        public PublicacionBl()
        {
            _repositorio = new Repositorio<RsocialPublicacion>();
        }
        public int CrearPublicacion(RsocialPublicacion publicacion) 
        {
            return _repositorio.Agregar(publicacion);
        }
    }
}
