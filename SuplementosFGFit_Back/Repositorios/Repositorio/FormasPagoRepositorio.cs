using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;

namespace SuplementosFGFit_Back.Repositorios.Repositorio
{
    public class FormasPagoRepositorio : Repositorio<FormasPago>, IFormasPagoRepositorio
    {
        private readonly SuplementosFgfitContext _db;
        public FormasPagoRepositorio(SuplementosFgfitContext db) : base(db)
        {
            _db = db;
        }

        public async Task<FormasPago> Actualizar(FormasPago forma)
        {
            _db.FormasPagos.Update(forma);
            await _db.SaveChangesAsync();
            return forma;
        }
    }
}
