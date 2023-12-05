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
    public class TallaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MyDbContext _dbContext;

        public TallaController(IMapper mapper, MyDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<TallaDTO>>();
            var listaDTO = new List<TallaDTO>();

            try
            {
                var lista = await _dbContext.CatTallas.ToListAsync();

                listaDTO = _mapper.Map<List<TallaDTO>>(lista);

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
