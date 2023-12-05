using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class CatProducto
{
    public int IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public double? PrecioVenta { get; set; }

    public virtual ICollection<TblDetalleInventario> TblDetalleInventarios { get; set; } = new List<TblDetalleInventario>();
}
