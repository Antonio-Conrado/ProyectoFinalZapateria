using TiendaZapateria.Shared;

namespace TiendaZapateria.Client.Services.Contrato
{
	public interface IRolService
	{
        Task<List<RolDTO>> Lista();
        Task<RolDTO> Buscar(int id);
        Task<int> Guardar(RolDTO entidad);

        Task<bool> Eliminar(int id);

        Task<bool> Editar(RolDTO entidad);
        Task<bool> VerificarUso(int id);
    }
}
