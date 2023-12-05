using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaZapateria.Server.Models;
using TiendaZapateria.Shared;

using Microsoft.EntityFrameworkCore;

namespace TiendaZapateria.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MyDbContext _dbContext;

        public RolController(IMapper mapper, MyDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<RolDTO>>();
            var listaRolDTO = new List<RolDTO>();

            try
            {
                var listaRoles = await _dbContext.TblRols.ToListAsync();

                listaRolDTO = _mapper.Map<List<RolDTO>>(listaRoles);

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaRolDTO;
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
        public async Task<IActionResult> Guardar(RolDTO rol)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {

                rol.NombreRol = rol.NombreRol.ToLower();
                TblRol _rol = _mapper.Map<TblRol>(rol);
                _dbContext.TblRols.Add(_rol);

                await _dbContext.SaveChangesAsync();

                if (_rol.IdRol != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = _rol.IdRol;

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
        public async Task<IActionResult> Editar([FromBody] RolDTO entidad)
        {
            var responseApi = new ResponseAPI<RolDTO>();

            try
            {
                var dbEntidad = await _dbContext.TblRols.FirstOrDefaultAsync(r => r.IdRol == entidad.IdRol);

                if (dbEntidad != null)
                {
                    _mapper.Map(entidad, dbEntidad);

                    _dbContext.TblRols.Update(dbEntidad);
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
            var responseApi = new ResponseAPI<RolDTO>();
            var RolDTO = new RolDTO();

            try
            {
                var dbRol = await _dbContext.TblRols.FirstOrDefaultAsync(x => x.IdRol == id);

                if (dbRol != null)
                {
                    RolDTO = _mapper.Map<RolDTO>(dbRol);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = RolDTO;

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

                var verificarExistencia = await _dbContext.TblUsuarios.AnyAsync(u => u.IdRol == id);
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
                    responseApi.Mensaje = "El Rol está siendo utilizado y no se puede eliminar.";
                }
                else
                {
                    var dbRol = await _dbContext.TblRols.FirstOrDefaultAsync(r => r.IdRol == id);

                    if (dbRol != null)
                    {
                        _dbContext.TblRols.Remove(dbRol);
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
