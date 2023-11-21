using SuplementosFGFit_Back.Models;
using System.Linq.Expressions;

namespace SuplementosFGFit_Back.Repositorios.IRepositorio
{
    public interface IProveedorFormaEnvioRepositorio : IRepositorio<ProveedoresXformaEnvio>
    {
        Task<ProveedoresXformaEnvio> Actualizar(ProveedoresXformaEnvio p);
        Task<List<ProveedoresXformaEnvio>> ObtenerIDList(Expression<Func<ProveedoresXformaEnvio, bool>>? filtro = null, bool tracked = true);
        Task<ProveedoresXformaEnvio> ObtenerIDProveedorFormaEnvio(Expression<Func<ProveedoresXformaEnvio, bool>>? filtro = null, bool tracked = true);
    }
}
