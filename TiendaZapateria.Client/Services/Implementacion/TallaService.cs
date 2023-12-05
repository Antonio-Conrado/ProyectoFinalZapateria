using System.Net.Http.Json;
using TiendaZapateria.Client.Services.Contrato;
using TiendaZapateria.Shared;
using static System.Net.WebRequestMethods;

namespace TiendaZapateria.Client.Services.Implementacion
{
    public class TallaService : ITallaService
    {
        private readonly HttpClient _http;
        public TallaService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<TallaDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<TallaDTO>>>("api/Talla/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }
    }
}
