using SuplementosFGFit_Back.Models;
using System.Linq.Expressions;

namespace SuplementosFGFit_Back.Repositorios.IRepositorio
{
    public interface IProveedorFormaPagoRepositorio : IRepositorio<ProveedoresXformaPago>
    {
        Task<ProveedoresXformaPago> Actualizar(ProveedoresXformaPago p);
        Task<List<ProveedoresXformaPago>> ObtenerIDList(Expression<Func<ProveedoresXformaPago, bool>>? filtro = null, bool tracked = true);
        Task<ProveedoresXformaPago> ObtenerIDProveedorFormaPago(Expression<Func<ProveedoresXformaPago, bool>>? filtro = null, bool tracked = true);
    }
}
