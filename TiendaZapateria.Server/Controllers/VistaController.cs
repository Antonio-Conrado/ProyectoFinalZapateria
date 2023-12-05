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
    public class VistaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MyDbContext _dbContext;

        public VistaController(IMapper mapper, MyDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<VistaDTO>>();
            var listaDTO = new List<VistaDTO>();

            try
            {
                var lista = await _dbContext.TblVista.ToListAsync();

                listaDTO = _mapper.Map<List<VistaDTO>>(lista);

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



        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(VistaDTO entidad)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {

                TblVista _entidad = _mapper.Map<TblVista>(entidad);
                _dbContext.TblVista.Add(_entidad);

                await _dbContext.SaveChangesAsync();

                if (_entidad.IdVista != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = _entidad.IdVista;

                    responseApi.Mensaje = "Guardado exitosamente";
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No guardado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }



        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] VistaDTO entidad)
        {
            var responseApi = new ResponseAPI<VistaDTO>();

            try
            {
                var dbEntidad = await _dbContext.TblVista.FirstOrDefaultAsync(r => r.IdVista == entidad.IdVista);

                if (dbEntidad != null)
                {
                    _mapper.Map(entidad, dbEntidad);

                    _dbContext.TblVista.Update(dbEntidad);
                    await _dbContext.SaveChangesAsync();
                    responseApi.Mensaje = "Editado correctamente";
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = entidad;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No encontrado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }




        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<VistaDTO>();
            var EntidadDTO = new VistaDTO();

            try
            {
                var dbEntidad = await _dbContext.TblVista.FirstOrDefaultAsync(x => x.IdVista == id);

                if (dbEntidad != null)
                {
                    EntidadDTO = _mapper.Map<VistaDTO>(dbEntidad);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = EntidadDTO;

                    responseApi.Mensaje = "Encontrado exitosamente";
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No encontrado";
                }

            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbEntidad = await _dbContext.TblVista.FirstOrDefaultAsync(r => r.IdVista == id);

                if (dbEntidad != null)
                {
                    _dbContext.TblVista.Remove(dbEntidad);
                    await _dbContext.SaveChangesAsync();

                    responseApi.Mensaje = "Eliminado correctamente";
                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No encontrado";
                }

            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }
    }
}
