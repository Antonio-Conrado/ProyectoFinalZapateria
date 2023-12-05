using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class TblCompra
{
    public int IdCompra { get; set; }

    public int IdProveedor { get; set; }

    public int IdUsuario { get; set; }

    public int? NumFactura { get; set; }

    public string? Lote { get; set; }

    public DateTime FechaCompra { get; set; }

    public double Subtotal { get; set; }

    public double? Iva { get; set; }

    public double? Descuento { get; set; }

    public double Total { get; set; }

    public bool? Estado { get; set; }

    public virtual CatProveedor IdProveedorNavigation { get; set; } = null!;

    public virtual TblUsuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<TblDetalleCompra> TblDetalleCompras { get; set; } = new List<TblDetalleCompra>();
}
