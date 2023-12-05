using TiendaZapateria.Shared;

namespace TiendaZapateria.Server.Repositorio.Contrato
{
    public interface IAutorizacionRepositorio
    {
        Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion);
        Task<AutorizacionResponse> DevolverRefreshToken(RefreshTokenRequest refreshTokenRequest, int idUsuario, string nombreRol);
        Task<string> ObtenerNombreRolDelUsuario(int idUsuario);
    }
}
