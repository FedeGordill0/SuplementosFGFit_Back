using SuplementosFGFit_Back.Models;

namespace SuplementosFGFit_Back.Repositorios.IRepositorio
{
    public interface IUnidadesMedidaRepositorio : IRepositorio<UnidadesMedidum>
    {
        Task<UnidadesMedidum> Actualizar(UnidadesMedidum u);
    }
}
