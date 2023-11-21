using System.Linq.Expressions;

namespace SuplementosFGFit_Back.Repositorios.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null);
        Task<T> ObtenerID(Expression<Func<T, bool>>? filtro = null, bool tracked = true);
        Task Crear(T entidad);
        Task Eliminar(T entidad);
        Task Grabar();
    }
}
