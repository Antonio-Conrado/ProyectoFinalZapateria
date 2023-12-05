namespace TiendaZapateria.Shared
{
    public class RefreshTokenRequest
    {
        public string TokenExpirado { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
