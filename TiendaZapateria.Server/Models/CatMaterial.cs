using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class CatMaterial
{
    public int IdMaterial { get; set; }

    public string NombreMaterial { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<TblDetalleInventario> TblDetalleInventarios { get; set; } = new List<TblDetalleInventario>();
}
