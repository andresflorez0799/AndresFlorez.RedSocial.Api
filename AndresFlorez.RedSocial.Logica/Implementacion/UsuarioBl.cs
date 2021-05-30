using AndresFlorez.RedSocial.Logica.Contrato;
using AndresFlorez.RedSocial.Modelo.EF;
using System.Linq;
using AndresFlorez.RedSocial.Datos;

namespace AndresFlorez.RedSocial.Logica.Implementacion
{
    public class UsuarioBl : BaseGenericoLogica<RsocialUsuario>, IUsuarioBl
    {
        private readonly IRepositorio<RsocialUsuario> _repositorio;

        public UsuarioBl() 
        {
            _repositorio = new Repositorio<RsocialUsuario>();
        }

        public RsocialUsuario GetByEmail(string email)
        {
            this.parametrosQuery.Where = s => s.Email == email;
            return _repositorio.GetCustomFilter(this.parametrosQuery).ToList().FirstOrDefault();
        }
    }
}
