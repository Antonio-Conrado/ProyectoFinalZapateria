using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class CatTalla
{
    public int IdTalla { get; set; }

    public double? TallaEuropea { get; set; }

    public double? TallaUs { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<TblDetalleInventario> TblDetalleInventarios { get; set; } = new List<TblDetalleInventario>();
}
