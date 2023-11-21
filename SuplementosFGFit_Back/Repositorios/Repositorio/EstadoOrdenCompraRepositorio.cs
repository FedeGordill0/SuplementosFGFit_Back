using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;

namespace SuplementosFGFit_Back.Repositorios.Repositorio
{
    public class EstadoOrdenCompraRepositorio : Repositorio<EstadoOrdenCompra>, IEstadoOrdenCompraRepositorio
    {
        private readonly SuplementosFgfitContext _db;
        public EstadoOrdenCompraRepositorio(SuplementosFgfitContext db) : base(db)
        {
            _db = db;
        }

        public async Task<EstadoOrdenCompra> Actualizar(EstadoOrdenCompra orden)
        {
            _db.EstadoOrdenCompras.Update(orden);
            await _db.SaveChangesAsync();
            return orden;
        }
    }
}
