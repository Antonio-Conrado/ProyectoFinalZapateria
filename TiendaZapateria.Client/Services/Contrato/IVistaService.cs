using TiendaZapateria.Shared;

namespace TiendaZapateria.Client.Services.Contrato
{
	public interface IVistaService
	{
        Task<List<VistaDTO>> Lista();
        Task<VistaDTO> Buscar(int id);
        Task<int> Guardar(VistaDTO entidad);

        Task<bool> Eliminar(int id);

        Task<bool> Editar(VistaDTO entidad);
    }
}
