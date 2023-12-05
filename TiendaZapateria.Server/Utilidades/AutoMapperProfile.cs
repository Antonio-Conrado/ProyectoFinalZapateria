using AutoMapper;
using TiendaZapateria.Server.Models;
using TiendaZapateria.Shared;
namespace TiendaZapateria.Server.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            #region usuario
            CreateMap<TblUsuario, UsuarioDTO>().ReverseMap();
            CreateMap<TblUsuario, registrarUsuarioDTO>().ReverseMap();

            #endregion usuario

            #region DetalleVenta

            CreateMap<TblDetalleVenta, DetalleVentaDTO>().ReverseMap();

            #endregion DetalleVenta

            #region Producto

            CreateMap<CatProducto, ProductoDTO>().ReverseMap();
            CreateMap<CatProducto, RegistrarProductoDTO>().ReverseMap();

            #endregion Producto

            #region Venta

            CreateMap<TblVenta, VentaDTO>().ReverseMap();

            #endregion Venta

            #region Categoria

            CreateMap<CatCategoria, CategoriaDTO>().ReverseMap();

            #endregion Categoria


            #region talla

            CreateMap<CatTalla, TallaDTO>().ReverseMap();

            #endregion talla

            #region marca

            CreateMap<CatMarca, MarcaDTO>().ReverseMap();
            #endregion marca




            #region rol

            CreateMap<TblRol, RolDTO>().ReverseMap();

            #endregion rol

            #region vista

            CreateMap<TblVista, VistaDTO>().ReverseMap();

            #endregion vista

            #region detalleinventario

            CreateMap<TblDetalleInventario, DetalleInventarioDTO>().ReverseMap();
            CreateMap<TblDetalleInventario, RegistrarDetalleInventarioDTO>().ReverseMap();

            #endregion detalleinventario

            #region inventario

            CreateMap<TblInventario, InventarioDTO>().ReverseMap();

            CreateMap<TblInventario, RegistrarInventarioDTO>().ReverseMap();


            #endregion inventario


            #region vista

            CreateMap<TblVista, VistaDTO>().ReverseMap();

            #endregion vista

        }

    }
}
