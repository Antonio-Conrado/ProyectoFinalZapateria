using System.Net.Http.Json;
using TiendaZapateria.Client.Pages;
using TiendaZapateria.Client.Services.Contrato;
using TiendaZapateria.Shared;

namespace TiendaZapateria.Client.Services.Implementacion
{
	public class RolService : IRolService
	{
        private readonly HttpClient _http;
        public RolService(HttpClient http)
        {
            _http = http;
        }

        public async Task<RolDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<RolDTO>>($"api/Rol/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<bool> Editar(RolDTO entidad)
        {
            var result = await _http.PutAsJsonAsync($"api/Rol/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<RolDTO>>();

            if (response != null && response.EsCorrecto)
            {
                return true;
            }
            else
            {
                throw new Exception(response?.Mensaje ?? "Hubo un problema al editar el campo.");
            }
        }

        public async  Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Rol/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<int> Guardar(RolDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/Rol/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<List<RolDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<RolDTO>>>("api/Rol/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<bool> VerificarUso(int id)
        {
            try
            {
                var response = await _http.GetAsync($"api/Rol/VerificarUso/{id}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<ResponseAPI<bool>>();

                return result?.EsCorrecto == true && result.Valor == true;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al verificar uso de Rol: {ex.Message}");
            }
        }
    }
}
