using SuplementosFGFit_Back.Models;
using System.Linq.Expressions;

namespace SuplementosFGFit_Back.Repositorios.IRepositorio
{
    public interface IProductoProveedorRepositorio : IRepositorio<ProductosXproveedore>
    {
        Task<ProductosXproveedore> Actualizar(ProductosXproveedore entidad);
        Task<List<ProductosXproveedore>> ObtenerIDList(Expression<Func<ProductosXproveedore, bool>>? filtro = null, bool tracked = true);
        Task<ProductosXproveedore> ObtenerIDProductoProveedor(Expression<Func<ProductosXproveedore, bool>>? filtro = null, bool tracked = true);
    }
}
