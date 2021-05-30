using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndresFlorez.RedSocial.Datos;

namespace AndresFlorez.RedSocial.Logica
{
    public abstract class BaseGenericoLogica<T>
    {
        protected ParametrosQuery<T> parametrosQuery;

        public BaseGenericoLogica() 
        {
            parametrosQuery = new ParametrosQuery<T>();
        }

    }
}
