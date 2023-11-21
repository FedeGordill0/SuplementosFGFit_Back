using SuplementosFGFit_Back.Models;

namespace SuplementosFGFit_Back.Repositorios.IRepositorio
{
    public interface IEstadoOrdenCompraRepositorio : IRepositorio<EstadoOrdenCompra>
    {
        Task<EstadoOrdenCompra> Actualizar(EstadoOrdenCompra orden);
    }
}
