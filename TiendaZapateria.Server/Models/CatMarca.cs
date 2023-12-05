using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class CatMarca
{
    public int IdMarca { get; set; }

    public string NombreMarca { get; set; } = null!;

    public bool? Estado { get; set; }

    public virtual ICollection<TblDetalleInventario> TblDetalleInventarios { get; set; } = new List<TblDetalleInventario>();
}
