using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class TblDetalleVenta
{
    public int IdDetalleVenta { get; set; }

    public int? IdVenta { get; set; }

    public int IdInventario { get; set; }

    public int Cantidad { get; set; }

    public int PrecioVenta { get; set; }

    public int? Descuento { get; set; }

    public int SubTotal { get; set; }

    public bool? Estado { get; set; }

    public virtual TblInventario IdInventarioNavigation { get; set; } = null!;

    public virtual TblVenta? IdVentaNavigation { get; set; }
}
