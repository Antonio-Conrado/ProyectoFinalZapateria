using System.Net.Http.Json;
using TiendaZapateria.Client.Pages;
using TiendaZapateria.Client.Pages.Modales;
using TiendaZapateria.Client.Services.Contrato;
using TiendaZapateria.Shared;

namespace TiendaZapateria.Client.Services.Implementacion
{
	public class ProductoService : IProductoService
	{
        private readonly HttpClient _http;
        public ProductoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<RegistrarInventarioDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<RegistrarInventarioDTO>>($"api/Inventario/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<bool> Editar(RegistrarInventarioDTO entidad)
        {
            var result = await _http.PutAsJsonAsync($"api/Inventario/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<RegistrarInventarioDTO>>();

            if (response != null && response.EsCorrecto)
            {
                return true;
            }
            else
            {
                throw new Exception(response?.Mensaje ?? "Hubo un problema al editar la campo.");
            }
        }

        public  async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Inventario/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<int> Guardar(RegistrarInventarioDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/Inventario/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<List<InventarioDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<InventarioDTO>>>("api/Inventario/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<bool> VerificarUso(int id)
        {
            try
            {
                var response = await _http.GetAsync($"api/Inventario/VerificarUso/{id}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<ResponseAPI<bool>>();

                return result?.EsCorrecto == true && result.Valor == true;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al verificar uso de Producto: {ex.Message}");
            }
        }
    }
}
