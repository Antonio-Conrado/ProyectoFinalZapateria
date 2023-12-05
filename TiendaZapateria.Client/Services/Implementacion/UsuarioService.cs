using TiendaZapateria.Client.Services.Contrato;
using TiendaZapateria.Shared;
using System.Net.Http.Json;
using TiendaZapateria.Client.Pages.Login;
using TiendaZapateria.Client.Pages;

namespace TiendaZapateria.Client.Services.Implementacion
{
    public class UsuarioService : IUsuarioService
    {

        private readonly HttpClient _http;
        public UsuarioService(HttpClient http)
        {
            _http = http;
        }
        public async Task<UsuarioDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<UsuarioDTO>>($"api/Usuario/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        
        public async Task<bool> Editar(registrarUsuarioDTO entidad)
        {
            var result = await _http.PutAsJsonAsync($"api/Usuario/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<registrarUsuarioDTO>>();

            if (response != null && response.EsCorrecto)
            {
                return true;
            }
            else
            {
                throw new Exception(response?.Mensaje ?? "Hubo un problema al editar el campo.");
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Usuario/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }

        

        public async Task<int> Guardar(registrarUsuarioDTO usuario)
        {
            var result = await _http.PostAsJsonAsync("api/Usuario/Guardar", usuario);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<AutorizacionResponse> IniciarSesion(AutorizacionRequest Login)
        {
            var response = await _http.PostAsJsonAsync("api/Usuario/Autenticar", Login);


            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AutorizacionResponse>();
            }
            else
            {
                // Manejar el escenario de error, lanzar excepción, retornar un valor por defecto, etc.
                // Por ejemplo:
                throw new Exception("Error en la autenticación");
            }
        }

        public async Task<List<UsuarioDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<UsuarioDTO>>>("api/Usuario/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<bool> VerificarUso(int id)
        {
            try
            {
                var response = await _http.GetAsync($"api/Usuario/VerificarUso/{id}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<ResponseAPI<bool>>();

                return result?.EsCorrecto == true && result.Valor == true;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al verificar uso de Usuario: {ex.Message}");
            }
        }
    }
}
