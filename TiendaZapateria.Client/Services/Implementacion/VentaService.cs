using System.Net.Http.Json;
using TiendaZapateria.Client.Services.Contrato;
using TiendaZapateria.Shared;

namespace TiendaZapateria.Client.Services.Implementacion
{
    public class VentaService : IVentaService
    {
        private readonly HttpClient _http;
        public VentaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> Guardar(VentaDTO ventaDto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Venta/Registrar", ventaDto);
                var resultado = response.IsSuccessStatusCode;

                return resultado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> ObtenerUltimoNumeroFactura()
        {
            try
            {
                var response = await _http.GetAsync("api/Venta/UltimoNumeroFactura");
                response.EnsureSuccessStatusCode();

                var ultimoNumeroFactura = await response.Content.ReadFromJsonAsync<int>();
                return ultimoNumeroFactura;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el último número de factura", ex);
            }
        }
    }
}
