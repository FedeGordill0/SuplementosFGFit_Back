using Microsoft.EntityFrameworkCore;
using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;
using System.Linq.Expressions;
using System.Linq;

namespace SuplementosFGFit_Back.Repositorios.Repositorio
{
    public class ProveedorFormaPagoRepositorio : Repositorio<ProveedoresXformaPago>, IProveedorFormaPagoRepositorio
    {
        private readonly SuplementosFgfitContext _db;
        public ProveedorFormaPagoRepositorio(SuplementosFgfitContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ProveedoresXformaPago> Actualizar(ProveedoresXformaPago p)
        {
            _db.ProveedoresXformaPagos.Update(p);
            await _db.SaveChangesAsync();
            return p;
        }
        public async Task<List<ProveedoresXformaPago>> ObtenerIDList(Expression<Func<ProveedoresXformaPago, bool>>? filtro = null, bool tracked = true)
        {
            IQueryable<ProveedoresXformaPago> query = _dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            query = query.Include("IdProveedorNavigation").Include("IdFormaPagoNavigation");
            return await query.ToListAsync();
        }

        public async Task<ProveedoresXformaPago> ObtenerIDProveedorFormaPago(Expression<Func<ProveedoresXformaPago, bool>>? filtro = null, bool tracked = true)
        {
            IQueryable<ProveedoresXformaPago> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            query = query.Include("IdProveedorNavigation").Include("IdFormaPagoNavigation");
            return await query.FirstOrDefaultAsync();
        }
    }
}
