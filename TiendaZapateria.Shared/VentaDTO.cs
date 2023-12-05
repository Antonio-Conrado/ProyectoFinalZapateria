using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaZapateria.Shared
{
    public class VentaDTO
    {
        public int IdUsuario { get; set; }

        public int? NumeroFactura { get; set; }

        public string? NombreCliente { get; set; }

        public DateTime FechaVenta { get; set; }

        public int SubTotal { get; set; }

        public double? Iva { get; set; }

        public int? Descuento { get; set; }

        public double Total { get; set; }

        public bool? Estado { get; set; }

        //public virtual TblUsuario IdUsuarioNavigation { get; set; } = null!;

        public virtual ICollection<DetalleVentaDTO> TblDetalleVenta { get; set; } = new List<DetalleVentaDTO>();
    }
}
