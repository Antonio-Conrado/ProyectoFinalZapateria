using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class CatColor
{
    public int IdColor { get; set; }

    public string NombreColor { get; set; } = null!;

    public bool? Estado { get; set; }

    public virtual ICollection<TblDetalleInventario> TblDetalleInventarios { get; set; } = new List<TblDetalleInventario>();
}
