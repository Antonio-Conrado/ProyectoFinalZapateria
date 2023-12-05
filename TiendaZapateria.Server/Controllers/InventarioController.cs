using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaZapateria.Server.Models;
using TiendaZapateria.Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TiendaZapateria.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MyDbContext _dbContext;

        public InventarioController(IMapper mapper, MyDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        //[Authorize(Roles = "Administrador,Empleado")]
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<InventarioDTO>>();
            var listaDTO = new List<InventarioDTO>();

            try
            {
                var query = await _dbContext.TblInventarios
                    .Include(i => i.IdDetalleInventarioNavigation)
                        .ThenInclude(detalle => detalle.IdCategoriaNavigation)
                    .Include(i => i.IdDetalleInventarioNavigation)
                        .ThenInclude(detalle => detalle.IdProductoNavigation)
                    .Include(i => i.IdDetalleInventarioNavigation)
                        .ThenInclude(detalle => detalle.IdMarcaNavigation)
                    .Include(i => i.IdDetalleInventarioNavigation)
                        .ThenInclude(detalle => detalle.IdTallaNavigation)
                    .ToListAsync();

                listaDTO = _mapper.Map<List<InventarioDTO>>(query);

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaDTO;
                responseApi.Mensaje = "Lista cargada correctamente";
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Error al cargar la lista de inventario: " + ex.Message;
            }

            return Ok(responseApi);
        }


        //[Authorize(Roles = "Administrador,Empleado")]
        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(RegistrarInventarioDTO entidad)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {

                TblInventario _entidad = _mapper.Map<TblInventario>(entidad);
                _dbContext.TblInventarios.Add(_entidad);

                await _dbContext.SaveChangesAsync();

                if (_entidad.IdInventario != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = _entidad.IdInventario;

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
        public async Task<IActionResult> Editar([FromBody] RegistrarInventarioDTO entidad)
        {
            var responseApi = new ResponseAPI<RegistrarInventarioDTO>();

            try
            {
                var dbEntidad = await _dbContext.TblInventarios
                                  .Include(x => x.IdDetalleInventarioNavigation)
                                      .ThenInclude(detalle => detalle.IdProductoNavigation)
                                  .FirstOrDefaultAsync(x => x.IdInventario == entidad.IdInventario);

                if (dbEntidad != null)
                {
                    _mapper.Map(entidad, dbEntidad);

                   

                    _dbContext.TblInventarios.Update(dbEntidad);
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
            var responseApi = new ResponseAPI<RegistrarInventarioDTO>();
            var EntidadDTO = new RegistrarInventarioDTO();

            try
            {
                var dbEntidad = await _dbContext.TblInventarios
                                 .Include(x => x.IdDetalleInventarioNavigation)
                                     .ThenInclude(detalle => detalle.IdProductoNavigation)
                                 .FirstOrDefaultAsync(x => x.IdInventario == id);

                if (dbEntidad != null)
                {
                    EntidadDTO = _mapper.Map<RegistrarInventarioDTO>(dbEntidad);

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

                var verificarExistencia = await _dbContext.TblDetalleVenta.AnyAsync(u => u.IdInventario == id);
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

                if (responseVerificarUso is ObjectResult objectResult && objectResult.Value is ResponseAPI<bool> uso && uso.EsCorrecto && uso.Valor)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "El Producto está siendo utilizado y no se puede eliminar.";
                }
                else
                {
                    var dbEntidad = await _dbContext.TblInventarios.FirstOrDefaultAsync(r => r.IdInventario == id);

                    if (dbEntidad != null)
                    {
                        _dbContext.TblInventarios.Remove(dbEntidad);
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
