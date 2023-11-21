namespace SuplementosFGFit_Back.Models.Custom
{
    public class AutorizacionResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Resultado { get; set; }
        public string Mensaje { get; set; }
        //--------------------------------------
        public string Rol { get; set; } // Agregar la propiedad del rol
    }
}
