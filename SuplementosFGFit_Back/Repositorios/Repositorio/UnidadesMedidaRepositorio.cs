using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;

namespace SuplementosFGFit_Back.Repositorios.Repositorio
{
    public class UnidadesMedidaRepositorio : Repositorio<UnidadesMedidum>, IUnidadesMedidaRepositorio
    {
        private readonly SuplementosFgfitContext _db;
        public UnidadesMedidaRepositorio(SuplementosFgfitContext db) : base(db)
        {
            _db = db;
        }

        public async Task<UnidadesMedidum> Actualizar(UnidadesMedidum u)
        {
            _db.UnidadesMedida.Update(u);
            await _db.SaveChangesAsync();
            return u;
        }
    }
}
