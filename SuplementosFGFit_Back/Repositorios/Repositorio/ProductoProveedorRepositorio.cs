using Microsoft.EntityFrameworkCore;
using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;
using System.Linq.Expressions;

namespace SuplementosFGFit_Back.Repositorios.Repositorio
{
    public class ProductoProveedorRepositorio : Repositorio<ProductosXproveedore>, IProductoProveedorRepositorio
    {
        private readonly SuplementosFgfitContext _db;
        public ProductoProveedorRepositorio(SuplementosFgfitContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ProductosXproveedore> Actualizar(ProductosXproveedore entidad)
        {
            _db.ProductosXproveedores.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }


        public async Task<List<ProductosXproveedore>> ObtenerIDList(Expression<Func<ProductosXproveedore, bool>>? filtro = null, bool tracked = true)
        {
            IQueryable<ProductosXproveedore> query = _dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            query = query.Include("IdProveedorNavigation").Include("IdProductoNavigation");
            return await query.ToListAsync();
        }

        public async Task<ProductosXproveedore> ObtenerIDProductoProveedor(Expression<Func<ProductosXproveedore, bool>>? filtro = null, bool tracked = true)
        {
            IQueryable<ProductosXproveedore> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            query = query.Include("IdProveedorNavigation").Include("IdProductoNavigation");
            return await query.FirstOrDefaultAsync();
        }
    }
}
