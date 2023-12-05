using TiendaZapateria.Shared;

namespace TiendaZapateria.Client.Services.Contrato
{
	public interface ICategoriaService
	{
		Task<List<CategoriaDTO>> Lista();
		Task<CategoriaDTO> Buscar(int id);
		Task<int> Guardar(CategoriaDTO categoria);
		
		Task<bool> Eliminar(int id);

        Task<bool> Editar(CategoriaDTO categoria);
        Task<bool> VerificarUso(int id);
    }
}
