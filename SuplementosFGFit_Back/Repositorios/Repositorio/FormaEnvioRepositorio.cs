using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;

namespace SuplementosFGFit_Back.Repositorios.Repositorio
{
    public class FormaEnvioRepositorio : Repositorio<FormasEnvio>, IFormaEnvioRepositorio
    {
        private readonly SuplementosFgfitContext _db;
        public FormaEnvioRepositorio(SuplementosFgfitContext db) : base(db)
        {
            _db = db;
        }

        public async Task<FormasEnvio> Actualizar(FormasEnvio form)
        {
            _db.FormasEnvios.Update(form);
            await _db.SaveChangesAsync();
            return form;
        }
    }
}
