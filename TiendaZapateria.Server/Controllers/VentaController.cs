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
    public class VentaController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly MyDbContext _dbContext;
        public VentaController(IMapper mapper, MyDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        //[Authorize(Roles = "Administrador,Empleado")]
        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar(VentaDTO ventaDto)
        {
            var responseApi = new ResponseAPI<VentaDTO>();
            try
            {
                var mdVenta = _mapper.Map<TblVenta>(ventaDto);
                var mdDetalleVenta = _mapper.Map<List<TblDetalleVenta>>(ventaDto.TblDetalleVenta);

                mdVenta.TblDetalleVenta = mdDetalleVenta;

                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var detalle in mdDetalleVenta)
                        {
                            var producto = _dbContext.TblInventarios.FirstOrDefault(p => p.IdInventario == detalle.IdInventario);

                            if (producto != null)
                            {
                                // Restar la cantidad vendida del stock del producto
                                producto.Existencia -= detalle.Cantidad;
                                _dbContext.TblInventarios.Update(producto);
                            }
                            else
                            {
                                // Manejar el caso en el que no se encuentre el producto
                                transaction.Rollback();
                                responseApi.EsCorrecto = false;
                                responseApi.Mensaje = $"El producto con ID {detalle.IdInventario} no existe";
                                return NotFound(responseApi);
                            }
                        }

                        _dbContext.Set<TblVenta>().Add(mdVenta);
                        await _dbContext.SaveChangesAsync();

                        transaction.Commit();

                        responseApi.EsCorrecto = true;
                        responseApi.Mensaje = "Venta registrada exitosamente";
                        responseApi.Valor = ventaDto;
                        return Ok(responseApi);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        responseApi.EsCorrecto = false;
                        responseApi.Mensaje = "No se registro la venta";
                        return BadRequest(responseApi);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("UltimoNumeroFactura")]
        public async Task<IActionResult> ObtenerUltimoNumeroFactura()
        {
            try
            {
                var ultimaVenta = await _dbContext.TblVenta.OrderByDescending(v => v.IdVenta).FirstOrDefaultAsync();

                int ultimoNumeroFactura;

                if (ultimaVenta != null)
                {
                    ultimoNumeroFactura = ultimaVenta.NumeroFactura ?? 0;
                    ultimoNumeroFactura++; 
                }
                else
                {
                    ultimoNumeroFactura = 1; 
                }

                return Ok(ultimoNumeroFactura);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
