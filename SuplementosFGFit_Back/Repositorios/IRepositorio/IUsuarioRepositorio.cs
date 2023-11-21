using SuplementosFGFit_Back.Models;

namespace SuplementosFGFit_Back.Repositorios.IRepositorio
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Task<Usuario> Actualizar(Usuario u);
    }
}
