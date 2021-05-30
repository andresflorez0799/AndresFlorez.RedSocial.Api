using AndresFlorez.RedSocial.Datos;
using AndresFlorez.RedSocial.Logica.Contrato;
using AndresFlorez.RedSocial.Modelo.EF;
using System.Collections.Generic;
using System.Linq;

namespace AndresFlorez.RedSocial.Logica.Implementacion
{
    public class PublicacionBl : BaseGenericoLogica<RsocialPublicacion>, IPublicacionBl
    {
        private readonly IRepositorio<RsocialPublicacion> _repositorio;

        public PublicacionBl()
        {
            _repositorio = new Repositorio<RsocialPublicacion>();
        }
        public int CrearPublicacion(RsocialPublicacion publicacion)
        {
            if (publicacion != null && !string.IsNullOrEmpty(publicacion.Texto))
                return _repositorio.Agregar(publicacion);
            return 0;
        }

        public IEnumerable<RsocialPublicacion> ConsultarPublicacion()
        {
            System.DateTime fechaFiltro = System.DateTime.Now.AddDays(-15);

            this.parametrosQuery.Where = s => s.Fecha >= fechaFiltro;
            return _repositorio.GetCustomFilter(this.parametrosQuery, 
                new string[] { "RsocialPublicacionArchivos", "RsocialPublicacionImagens", "RsocialPublicacionVideos" }).ToList();
        }
    }
}
