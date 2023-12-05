using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class TblDetalleCompra
{
    public int IdDetalleCompra { get; set; }

    public int IdCompra { get; set; }

    public int IdInventario { get; set; }

    public int CantidadProducto { get; set; }

    public double PrecioUnitario { get; set; }

    public double Subtotal { get; set; }

    public double? Descuento { get; set; }

    public double Total { get; set; }

    public bool? Estado { get; set; }

    public virtual TblCompra IdCompraNavigation { get; set; } = null!;

    public virtual TblInventario IdInventarioNavigation { get; set; } = null!;
}
