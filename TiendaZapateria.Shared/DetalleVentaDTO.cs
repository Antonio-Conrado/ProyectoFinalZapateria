using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaZapateria.Shared
{
    public class DetalleVentaDTO
    {
        public int IdDetalleVenta { get; set; }

        public int? IdVenta { get; set; }

        public int IdInventario { get; set; }

        public int Cantidad { get; set; }

        public int PrecioVenta { get; set; }

        public int? Descuento { get; set; }

        public int SubTotal { get; set; }

        public bool? Estado { get; set; }
    }
}
