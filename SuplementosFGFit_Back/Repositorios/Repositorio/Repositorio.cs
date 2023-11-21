using Microsoft.EntityFrameworkCore;
using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;
using System.Linq.Expressions;

namespace SuplementosFGFit_Back.Repositorios.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly SuplementosFgfitContext _db;
        internal DbSet<T> _dbSet;

        public Repositorio(SuplementosFgfitContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task Crear(T entidad)
        {
            await _dbSet.AddAsync(entidad);
            await Grabar();
        }

        public async Task Eliminar(T entidad)
        {
            _dbSet.Remove(entidad);
            await Grabar();
        }

        public async Task Grabar()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<T> ObtenerID(Expression<Func<T, bool>>? filtro = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = _dbSet;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            return await query.ToListAsync();
        }
    }
}
