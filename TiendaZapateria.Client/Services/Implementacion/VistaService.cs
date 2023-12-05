using System.Net.Http.Json;
using TiendaZapateria.Client.Pages;
using TiendaZapateria.Client.Services.Contrato;
using TiendaZapateria.Shared;

namespace TiendaZapateria.Client.Services.Implementacion
{
	public class VistaService : IVistaService
	{
        private readonly HttpClient _http;
        public VistaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<VistaDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<VistaDTO>>($"api/Vista/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<bool> Editar(VistaDTO entidad)
        {
            var result = await _http.PutAsJsonAsync($"api/Vista/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<VistaDTO>>();

            if (response != null && response.EsCorrecto)
            {
                return true;
            }
            else
            {
                throw new Exception(response?.Mensaje ?? "Hubo un problema al editar la campo.");
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Vista/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<int> Guardar(VistaDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/Vista/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<List<VistaDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<VistaDTO>>>("api/Vista/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        
    }
}
