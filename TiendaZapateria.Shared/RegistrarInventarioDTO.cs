using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaZapateria.Shared
{
    public class RegistrarInventarioDTO
    {

        public int IdInventario { get; set; }
        public double PrecioCompra { get; set; }

        public int Existencia { get; set; }

        public bool Descuento { get; set; }

        public int DescuentoMax { get; set; }

        public DateTime FechaIngreso { get; set; }

        public int? CodigoBarra { get; set; }

        public string? Lote { get; set; }

        public bool? Estado { get; set; }

        public virtual RegistrarDetalleInventarioDTO IdDetalleInventarioNavigation { get; set; } = null!;

    }
}
