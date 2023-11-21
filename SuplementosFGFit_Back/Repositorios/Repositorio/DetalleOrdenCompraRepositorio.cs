using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;

namespace SuplementosFGFit_Back.Repositorios.Repositorio
{
    public class DetalleOrdenCompraRepositorio : Repositorio<DetalleOrden>, IDetalleOrdenCompraRepositorio
    {
        private readonly SuplementosFgfitContext _db;
        public DetalleOrdenCompraRepositorio(SuplementosFgfitContext db) : base(db)
        {
            _db = db;
        }

        public async Task<DetalleOrden> Actualizar(DetalleOrden orden)
        {
            _db.DetalleOrdens.Update(orden);
            await _db.SaveChangesAsync();
            return orden;
        }
    }
}
