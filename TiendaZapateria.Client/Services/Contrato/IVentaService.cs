using TiendaZapateria.Shared;

namespace TiendaZapateria.Client.Services.Contrato
{
	public interface IVentaService
	{
        Task<bool> Guardar(VentaDTO ventaDto);

        Task<int> ObtenerUltimoNumeroFactura();
    }
}
