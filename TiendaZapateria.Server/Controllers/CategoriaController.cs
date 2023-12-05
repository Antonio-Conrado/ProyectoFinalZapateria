using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaZapateria.Server.Models;
using TiendaZapateria.Shared;

namespace TiendaZapateria.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MyDbContext _dbContext;

        public CategoriaController(IMapper mapper, MyDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        //[Authorize(Roles = "Administrador")]
        //[Authorize]
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<CategoriaDTO>>();
            var listaDTO = new List<CategoriaDTO>();

            try
            {
                var lista = await _dbContext.CatCategoria.ToListAsync();

                listaDTO = _mapper.Map<List<CategoriaDTO>>(lista);

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
        public async Task<IActionResult> Guardar(CategoriaDTO entidad)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {

                entidad.NombreCategoria = entidad.NombreCategoria.ToLower();
                CatCategoria _entidad = _mapper.Map<CatCategoria>(entidad);
                _dbContext.CatCategoria.Add(_entidad);

                await _dbContext.SaveChangesAsync();

                if (_entidad.IdCategoria != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = _entidad.IdCategoria;

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
        public async Task<IActionResult> Editar([FromBody] CategoriaDTO entidad)
        {
            var responseApi = new ResponseAPI<CategoriaDTO>();

            try
            {
                var dbEntidad = await _dbContext.CatCategoria.FirstOrDefaultAsync(r => r.IdCategoria == entidad.IdCategoria);

                if (dbEntidad != null)
                {
                    _mapper.Map(entidad, dbEntidad);

                    _dbContext.CatCategoria.Update(dbEntidad);
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
            var responseApi = new ResponseAPI<CategoriaDTO>();
            var EntidadDTO = new CategoriaDTO();

            try
            {
                var dbEntidad = await _dbContext.CatCategoria.FirstOrDefaultAsync(x => x.IdCategoria == id);

                if (dbEntidad != null)
                {
                    EntidadDTO = _mapper.Map<CategoriaDTO>(dbEntidad);

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






        [HttpGet]
        [Route("VerificarUso/{id}")]
        public async Task<IActionResult> VerificarUso(int id)
        {
            var responseApi = new ResponseAPI<bool>();

            try
            {

                var verificarExistencia= await _dbContext.TblDetalleInventarios.AnyAsync(u => u.IdCategoria == id);
                if (verificarExistencia == true)
                {
                    responseApi.Mensaje = "El elemento está siendo utilizado en otra tabla";
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = verificarExistencia;
                }
                else
                {
                    responseApi.Mensaje = "El elemento no está siendo utilizado en otra tabla o no existe";
                    responseApi.EsCorrecto = false;
                    responseApi.Valor = verificarExistencia;
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
                var responseVerificarUso = await VerificarUso(id);

                if (responseVerificarUso is ObjectResult objectResult && objectResult.Value is ResponseAPI<bool> usoCategoria && usoCategoria.EsCorrecto && usoCategoria.Valor)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "La categoria está siendo utilizado y no se puede eliminar.";
                }
                else
                {
                    var dbEntidad = await _dbContext.CatCategoria.FirstOrDefaultAsync(r => r.IdCategoria == id);
                    if (dbEntidad != null)
                    {
                        _dbContext.CatCategoria.Remove(dbEntidad);
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
