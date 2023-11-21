using SuplementosFGFit_Back.Models;

namespace SuplementosFGFit_Back.Repositorios.IRepositorio
{
    public interface IUsuarioRolRepositorio : IRepositorio<UsuariosXrole>
    {
        Task<UsuariosXrole> Actualizar(UsuariosXrole u);
    }
}
