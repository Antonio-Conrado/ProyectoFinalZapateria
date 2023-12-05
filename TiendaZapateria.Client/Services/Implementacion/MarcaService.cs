using System.Net.Http.Json;
using TiendaZapateria.Client.Services.Contrato;
using TiendaZapateria.Shared;
using static System.Net.WebRequestMethods;

namespace TiendaZapateria.Client.Services.Implementacion
{
    public class MarcaService : IMarcaService
    {
        private readonly HttpClient _http;
        public MarcaService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<MarcaDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<MarcaDTO>>>("api/Marca/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }
    }
}
