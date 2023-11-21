using SuplementosFGFit_Back.Models;

namespace SuplementosFGFit_Back.Repositorios.IRepositorio
{
    public interface IDetalleOrdenCompraRepositorio : IRepositorio<DetalleOrden>
    {
        Task<DetalleOrden> Actualizar(DetalleOrden orden);
    }
}
