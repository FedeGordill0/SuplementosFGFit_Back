namespace SuplementosFGFit_Back.Repositorios.IRepositorio
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string username, string password);
    }
}
