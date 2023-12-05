using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class TblInventario
{
    public int IdInventario { get; set; }

    public int IdDetalleInventario { get; set; }

    public double PrecioCompra { get; set; }

    public int Existencia { get; set; }

    public bool Descuento { get; set; }

    public int DescuentoMax { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public int? CodigoBarra { get; set; }

    public string? Lote { get; set; }

    public bool? Estado { get; set; }

    public virtual TblDetalleInventario IdDetalleInventarioNavigation { get; set; } = null!;

    public virtual ICollection<TblDetalleCompra> TblDetalleCompras { get; set; } = new List<TblDetalleCompra>();

    public virtual ICollection<TblDetalleVenta> TblDetalleVenta { get; set; } = new List<TblDetalleVenta>();

    public virtual ICollection<TblMovimientoInterno> TblMovimientoInternos { get; set; } = new List<TblMovimientoInterno>();
}
