using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;

namespace SuplementosFGFit_Back.Repositorios.Repositorio
{
    public class ProveedorRepositorio : Repositorio<Proveedore>, IProveedorRepositorio
    {
        private readonly SuplementosFgfitContext _db;
        public ProveedorRepositorio(SuplementosFgfitContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Proveedore> Actualizar(Proveedore p)
        {
            _db.Proveedores.Update(p);
            await _db.SaveChangesAsync();
            return p;
        }
    }
}
