using TiendaZapateria.Shared;

namespace TiendaZapateria.Client.Services.Contrato
{
	public interface IProductoService
	{
        Task<List<InventarioDTO>> Lista();
        Task<RegistrarInventarioDTO> Buscar(int id);
        Task<int> Guardar(RegistrarInventarioDTO entidad);

        Task<bool> Eliminar(int id);

        Task<bool> Editar(RegistrarInventarioDTO entidad);
        Task<bool> VerificarUso(int id);
    }
}
