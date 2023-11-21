using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;

namespace SuplementosFGFit_Back.Repositorios.Repositorio
{
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        private readonly SuplementosFgfitContext _db;
        public CategoriaRepositorio(SuplementosFgfitContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Categoria> Actualizar(Categoria categoria)
        {
            _db.Categorias.Update(categoria);
            await _db.SaveChangesAsync();
            return categoria;
        }
    }
}
