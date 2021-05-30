using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AndresFlorez.RedSocial.Modelo;
using AndresFlorez.RedSocial.Modelo.EF;
using Microsoft.EntityFrameworkCore;

namespace AndresFlorez.RedSocial.Datos
{
    public class Repositorio<T> : IRepositorio<T> where T : Entidad, new()
    {
        protected inalambria_redsocialContext _context = null;
        private DbSet<T> table = null;
        public Repositorio()
        {
            this._context = new inalambria_redsocialContext();
            table = _context.Set<T>();
        }

        public void Agregar(ICollection<T> entidades)
        {
            try
            {
                foreach (var item in entidades)
                {
                    _context.Entry(item).State = EntityState.Added;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public int Agregar(T entidad)
        {
            try
            {
                //_context.Entry(entidad).State = EntityState.Added;
                _context.Add(entidad);
                _context.SaveChanges();
                return entidad.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Actualizar(T entidad)
        {
            try
            {
                _context.Entry(entidad).State = EntityState.Modified;
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Eliminar(int id)
        {
            try
            {
                var entidad = Consultar(id);
                _context.Entry(entidad).State = EntityState.Deleted;
                int rows = _context.SaveChanges();
                _context.Entry(entidad).State = EntityState.Detached;
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T Consultar(int id)
        {
            try
            {
                return _context.Set<T>().Find(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<T> GetCustomFilter(ParametrosQuery<T> _param, string[] includes = null)
        {
            try
            {
                Expression<Func<T, bool>> whereTrue = x => true;
                var where = (_param.Where == null) ? whereTrue : _param.Where;

                if (includes != null && includes.Any()) 
                {
                    IQueryable<T> query = _context.Set<T>().Where(where);
                    foreach (var item in includes)
                    {
                        query = query.Include(item);
                    }
                    return query.AsNoTracking().ToList();
                    //return _context.Set<T>().Where(where).Include(string.Join(",", includes)).ToList();
                }
                else
                    return _context
                        .Set<T>()
                        .Where(where)
                        .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
