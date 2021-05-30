using System.Collections.Generic;

namespace AndresFlorez.RedSocial.Datos
{
    public interface IRepositorio<T> where T : class
    {
        IEnumerable<T> GetCustomFilter(ParametrosQuery<T> _param, string[] includes = null);
        void Agregar(ICollection<T> entidades);
        int Agregar(T entidad);
        int Eliminar(int id);
        int Actualizar(T entidad);
        T Consultar(int id);
    }
}
