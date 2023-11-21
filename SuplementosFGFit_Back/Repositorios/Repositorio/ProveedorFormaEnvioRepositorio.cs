using Microsoft.EntityFrameworkCore;
using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;
using System.Linq.Expressions;
using System.Linq;

namespace SuplementosFGFit_Back.Repositorios.Repositorio
{
    public class ProveedorFormaEnvioRepositorio : Repositorio<ProveedoresXformaEnvio>, IProveedorFormaEnvioRepositorio
    {
        private readonly SuplementosFgfitContext _db;
        public ProveedorFormaEnvioRepositorio(SuplementosFgfitContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ProveedoresXformaEnvio> Actualizar(ProveedoresXformaEnvio p)
        {
            _db.ProveedoresXformaEnvios.Update(p);
            await _db.SaveChangesAsync();
            return p;
        }
        public async Task<List<ProveedoresXformaEnvio>> ObtenerIDList(Expression<Func<ProveedoresXformaEnvio, bool>>? filtro = null, bool tracked = true)
        {
            IQueryable<ProveedoresXformaEnvio> query = _dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            query = query.Include("IdProveedorNavigation").Include("IdFormaEnvioNavigation");
            return await query.ToListAsync();
        }

        public async Task<ProveedoresXformaEnvio> ObtenerIDProveedorFormaEnvio(Expression<Func<ProveedoresXformaEnvio, bool>>? filtro = null, bool tracked = true)
        {
            IQueryable<ProveedoresXformaEnvio> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            query = query.Include("IdProveedorNavigation").Include("IdFormaEnvioNavigation");
            return await query.FirstOrDefaultAsync();
        }
    }
}
