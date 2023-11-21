using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;

namespace SuplementosFGFit_Back.Repositorios.Repositorio
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        private readonly SuplementosFgfitContext _db;
        public UsuarioRepositorio(SuplementosFgfitContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Usuario> Actualizar(Usuario u)
        {
            _db.Usuarios.Update(u);
            await _db.SaveChangesAsync();
            return u;
        }
    }
}
