using TiendaZapateria.Shared;

namespace TiendaZapateria.Client.Services.Contrato
{
	public interface ITallaService
	{
        Task<List<TallaDTO>> Lista();
    }
}
