using System;
using System.Linq.Expressions;

namespace AndresFlorez.RedSocial.Datos
{
    public class ParametrosQuery<T>
    {
        public ParametrosQuery()
        {
            Where = null;
        }
        public Expression<Func<T, bool>> Where { get; set; }
    }
}
