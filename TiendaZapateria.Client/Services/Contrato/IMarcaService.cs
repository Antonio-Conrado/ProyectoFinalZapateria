using TiendaZapateria.Shared;

namespace TiendaZapateria.Client.Services.Contrato
{
	public interface IMarcaService
	{
        Task<List<MarcaDTO>> Lista();
    }
}
