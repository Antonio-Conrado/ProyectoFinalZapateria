using System.Net.Http.Json;
using TiendaZapateria.Client.Pages;
using TiendaZapateria.Client.Services.Contrato;
using TiendaZapateria.Shared;

namespace TiendaZapateria.Client.Services.Implementacion
{
	public class CategoriaService : ICategoriaService
	{
		private readonly HttpClient _http;
		public CategoriaService(HttpClient http)
		{
			_http = http;
		}
		public async Task<CategoriaDTO> Buscar(int id)
		{
			var result = await _http.GetFromJsonAsync<ResponseAPI<CategoriaDTO>>($"api/Categoria/Buscar/{id}");

			if (result!.EsCorrecto)
				return result.Valor!;
			else
				throw new Exception(result.Mensaje); 
		}

        public async Task<bool> Editar(CategoriaDTO categoria)
        {

            var result = await _http.PutAsJsonAsync($"api/Categoria/Editar", categoria);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<CategoriaDTO>>();

            if (response != null && response.EsCorrecto)
            {
                return true;
            }
            else
            {
                throw new Exception(response?.Mensaje ?? "Hubo un problema al editar la categoría.");
            }
        }

        public async Task<bool> Eliminar(int id)
		{
			var result = await _http.DeleteAsync($"api/Categoria/Eliminar/{id}");
			var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

			if (response!.EsCorrecto)
				return response.EsCorrecto!;
			else
				throw new Exception(response.Mensaje);
		}

		public async Task<int> Guardar(CategoriaDTO categoria)
		{
			var result = await _http.PostAsJsonAsync("api/Categoria/Guardar", categoria);
			var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

			if (response!.EsCorrecto)
				return response.Valor!;
			else
				throw new Exception(response.Mensaje);
		}

		public async  Task<List<CategoriaDTO>> Lista()
		{
			var result = await _http.GetFromJsonAsync<ResponseAPI<List<CategoriaDTO>>>("api/Categoria/Lista");

			if (result!.EsCorrecto)
				return result.Valor!;
			else
				throw new Exception(result.Mensaje);
		}


        public async Task<bool> VerificarUso(int id)
        {
            try
            {
                var response = await _http.GetAsync($"api/Categoria/VerificarUso/{id}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<ResponseAPI<bool>>();

                return result?.EsCorrecto == true && result.Valor == true;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al verificar uso de Categoria: {ex.Message}");
            }
        }

        
    }
}
