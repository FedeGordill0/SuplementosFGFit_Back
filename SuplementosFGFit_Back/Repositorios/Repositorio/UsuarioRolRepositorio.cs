using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;

namespace SuplementosFGFit_Back.Repositorios.Repositorio
{
    public class UsuarioRolRepositorio : Repositorio<UsuariosXrole>, IUsuarioRolRepositorio
    {
        private readonly SuplementosFgfitContext _db;
        public UsuarioRolRepositorio(SuplementosFgfitContext db) : base(db)
        {
            _db = db;
        }

        public async Task<UsuariosXrole> Actualizar(UsuariosXrole u)
        {
            _db.UsuariosXroles.Update(u);
            await _db.SaveChangesAsync();
            return u;
        }
    }
}
