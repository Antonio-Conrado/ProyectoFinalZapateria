namespace TiendaZapateria.Shared
{
    public class AutorizacionResponse
    {
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public bool Resultado { get; set; }
        public string Msg { get; set; } = null!;
        public string nombreRol { get; set; } = null!;
        public int IdUsuario { get; set; }
    }
}
