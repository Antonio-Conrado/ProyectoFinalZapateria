using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaZapateria.Server.Models;
using TiendaZapateria.Shared;

namespace TiendaZapateria.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly MyDbContext _dbContext;

        public MarcaController(IMapper mapper, MyDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<MarcaDTO>>();
            var listaDTO = new List<MarcaDTO>();

            try
            {
                var lista = await _dbContext.CatMarcas.ToListAsync();

                listaDTO = _mapper.Map<List<MarcaDTO>>(lista);

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaDTO;
                responseApi.Mensaje = "Lista cargada correctamente";
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "No hay lista Disponibles";
            }

            return Ok(responseApi);
        }


    }
}
