using SuplementosFGFit_Back.Models;

namespace SuplementosFGFit_Back.Repositorios.IRepositorio
{
    public interface IProveedorRepositorio : IRepositorio<Proveedore>
    {
        Task<Proveedore> Actualizar(Proveedore p);
    }
}
