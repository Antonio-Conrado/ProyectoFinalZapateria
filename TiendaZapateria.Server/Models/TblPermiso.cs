using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class TblPermiso
{
    public int IdPermiso { get; set; }

    public string? NombrePermiso { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<TblRolPermiso> TblRolPermisos { get; set; } = new List<TblRolPermiso>();
}
