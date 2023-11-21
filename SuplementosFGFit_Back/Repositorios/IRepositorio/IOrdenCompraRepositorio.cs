using SuplementosFGFit_Back.Models;

namespace SuplementosFGFit_Back.Repositorios.IRepositorio
{
    public interface IOrdenCompraRepositorio : IRepositorio<OrdenesCompra>
    {
        Task<OrdenesCompra> Actualizar(OrdenesCompra orden);
    }
}
