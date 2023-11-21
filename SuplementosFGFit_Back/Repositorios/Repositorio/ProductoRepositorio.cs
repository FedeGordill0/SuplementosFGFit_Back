using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;

namespace SuplementosFGFit_Back.Repositorios.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly SuplementosFgfitContext _db;
        public ProductoRepositorio(SuplementosFgfitContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Producto> Actualizar(Producto p)
        {
            _db.Productos.Update(p);
            await _db.SaveChangesAsync();
            return p;
        }
    }
}
