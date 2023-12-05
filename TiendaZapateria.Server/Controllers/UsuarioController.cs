using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using TiendaZapateria.Server.Models;
using TiendaZapateria.Server.Repositorio.Contrato;
using TiendaZapateria.Shared;

namespace TiendaZapateria.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly MyDbContext _dbContext;
        private readonly IAutorizacionRepositorio _autorizacionRepositorio;

        public UsuarioController( IMapper mapper, MyDbContext dbContext,IAutorizacionRepositorio autorizacionRepositorio)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _autorizacionRepositorio = autorizacionRepositorio;
        }


        //[Authorize(Roles = "Administrador")]
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<UsuarioDTO>>();
            var listaDTO = new List<UsuarioDTO>();

            try
            {
                var lista = await _dbContext.TblUsuarios.Include(u => u.IdRolNavigation).ToListAsync();

                listaDTO = _mapper.Map<List<UsuarioDTO>>(lista);

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

        //[Authorize(Roles = "Administrador")]
        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(registrarUsuarioDTO entidad)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {

                TblUsuario _entidad = _mapper.Map<TblUsuario>(entidad);

              
                string claveAcceso = _entidad.ClaveAcceso; 
                string claveHash = BCrypt.Net.BCrypt.HashPassword(claveAcceso,12);

                _entidad.ClaveAcceso = claveHash;
                
                _dbContext.TblUsuarios.Add(_entidad);
                await _dbContext.SaveChangesAsync();

                if (_entidad.IdUsuario != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = _entidad.IdUsuario;
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



        //[Authorize(Roles = "Administrador")]

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] registrarUsuarioDTO entidad)
        {
            var responseApi = new ResponseAPI<registrarUsuarioDTO>();

            try
            {
                var dbEntidad = await _dbContext.TblUsuarios.FirstOrDefaultAsync(r => r.IdUsuario == entidad.IdUsuario);

                if (dbEntidad != null)
                {
                    _mapper.Map(entidad, dbEntidad);

                    string claveAcceso = entidad.ClaveAcceso;
                    string claveHash = BCrypt.Net.BCrypt.HashPassword(claveAcceso, 12);

                    dbEntidad.ClaveAcceso = claveHash;

                    _dbContext.TblUsuarios.Update(dbEntidad);
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
            var responseApi = new ResponseAPI<UsuarioDTO>();
            var EntidadDTO = new UsuarioDTO();

            try
            {
                var dbEntidad = await _dbContext.TblUsuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);

                if (dbEntidad != null)
                {
                    EntidadDTO = _mapper.Map<UsuarioDTO>(dbEntidad);

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

                var verificarExistencia = await _dbContext.TblVenta.AnyAsync(u => u.IdUsuario == id);

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

        //[Authorize(Roles = "Administrador")]
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
                    responseApi.Mensaje = "El usuario está siendo utilizado y no se puede eliminar.";
                }
                else
                {

                    var dbEntidad = await _dbContext.TblUsuarios.FirstOrDefaultAsync(r => r.IdUsuario == id);

                    if (dbEntidad != null)
                    {
                        _dbContext.TblUsuarios.Remove(dbEntidad);
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










      
        [HttpPost]
        [Route("Autenticar")]
        public async Task<IActionResult> Autenticar([FromBody] AutorizacionRequest autorizacion)
        {
            var resultado_autorizacion = await _autorizacionRepositorio.DevolverToken(autorizacion);
            if (resultado_autorizacion == null)
                return Unauthorized();


            

            return Ok(resultado_autorizacion);

        }


        [HttpPost]
        [Route("ObtenerRefreshToken")]
        public async Task<IActionResult> ObtenerRefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenExpiradoSupuestamente = tokenHandler.ReadJwtToken(request.TokenExpirado);

                if (tokenExpiradoSupuestamente.ValidTo > DateTime.UtcNow)
                    return BadRequest(new AutorizacionResponse { Resultado = false, Msg = "Token no ha expirado" });

                string idUsuario = tokenExpiradoSupuestamente.Claims.First(x =>
                    x.Type == JwtRegisteredClaimNames.NameId).Value.ToString();

                var nombreRol = await _autorizacionRepositorio.ObtenerNombreRolDelUsuario(int.Parse(idUsuario));
                
                
                var autorizacionResponse = await _autorizacionRepositorio.DevolverRefreshToken(request, int.Parse(idUsuario), nombreRol);

                if (autorizacionResponse.Resultado)
                    return Ok(autorizacionResponse);
                else
                    return BadRequest(autorizacionResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
