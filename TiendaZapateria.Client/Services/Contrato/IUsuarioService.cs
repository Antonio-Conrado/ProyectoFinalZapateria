using System.Threading.Tasks;
using TiendaZapateria.Shared;

namespace TiendaZapateria.Client.Services.Contrato
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> Lista();
        Task<UsuarioDTO> Buscar(int id);
        Task<int> Guardar(registrarUsuarioDTO usuario);
        
        Task<bool> Eliminar(int id);
        public Task<AutorizacionResponse> IniciarSesion(AutorizacionRequest Login);
        Task<bool> Editar(registrarUsuarioDTO entidad);
        Task<bool> VerificarUso(int id);

    }
}
