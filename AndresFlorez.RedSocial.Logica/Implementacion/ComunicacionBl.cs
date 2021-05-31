using AndresFlorez.RedSocial.Datos;
using AndresFlorez.RedSocial.Logica.Contrato;
using AndresFlorez.RedSocial.Modelo.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AndresFlorez.RedSocial.Logica.Implementacion
{
    public class ComunicacionBl : BaseGenericoLogica<RsocialMensajerium>, IComunicacionBl
    {
        private readonly IRepositorio<RsocialMensajerium> _repositorio;

        public ComunicacionBl()
        {
            _repositorio = new Repositorio<RsocialMensajerium>();
        }

        public void GuardarMensaje(RsocialMensajerium mensaje)
        {
            if (mensaje != null && !string.IsNullOrEmpty(mensaje.Mensaje))
                _repositorio.Agregar(mensaje);
        }

        public IEnumerable<RsocialMensajerium> ConsultarMensajes(int idUsuario)
        {
            DateTime fechaFiltro = DateTime.Now.AddDays(-15);

            this.parametrosQuery.Where = s => s.Fecha >= fechaFiltro 
                && (s.IdUsuarioOrigen == idUsuario || s.IdUsuarioDestino == idUsuario);
            return _repositorio.GetCustomFilter(this.parametrosQuery,
                new string[] { "IdUsuarioDestinoNavigation", "IdUsuarioOrigenNavigation" }).ToList();
        }

    }
}
