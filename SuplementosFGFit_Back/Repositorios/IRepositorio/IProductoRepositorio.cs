using SuplementosFGFit_Back.Models;

namespace SuplementosFGFit_Back.Repositorios.IRepositorio
{
    public interface IProductoRepositorio : IRepositorio<Producto>
    {
        Task<Producto> Actualizar(Producto p);
    }
}
