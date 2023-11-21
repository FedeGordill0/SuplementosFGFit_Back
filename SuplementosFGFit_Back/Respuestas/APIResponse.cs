using System.Net;

namespace SuplementosFGFit_Back.Respuesta
{
    public class APIResponse
    {
        public object Resultado { get; set; }
        public bool esExitoso { get; set; } = true;
        public HttpStatusCode StatusCode { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
