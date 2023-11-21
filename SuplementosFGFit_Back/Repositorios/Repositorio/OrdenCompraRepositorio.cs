using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;

namespace SuplementosFGFit_Back.Repositorios.Repositorio
{
    public class OrdenCompraRepositorio : Repositorio<OrdenesCompra>, IOrdenCompraRepositorio
    {
        private readonly SuplementosFgfitContext _db;
        public OrdenCompraRepositorio(SuplementosFgfitContext db) : base(db)
        {
            _db = db;
        }

        public async Task<OrdenesCompra> Actualizar(OrdenesCompra orden)
        {
            _db.OrdenesCompras.Update(orden);
            await _db.SaveChangesAsync();
            return orden;
        }
    }
}
