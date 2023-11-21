using SuplementosFGFit_Back.Repositorios.Repositorio;
using SuplementosFGFit_Back.Models;

namespace SuplementosFGFit_Back.Repositorios.IRepositorio
{
    public interface IFormasPagoRepositorio : IRepositorio<FormasPago>
    {
        Task<FormasPago> Actualizar(FormasPago forma);
    }
}
