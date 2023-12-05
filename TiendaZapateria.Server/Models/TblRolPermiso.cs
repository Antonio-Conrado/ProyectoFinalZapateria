using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class TblRolPermiso
{
    public int IdRolPermiso { get; set; }

    public int IdRol { get; set; }

    public int IdPermiso { get; set; }

    public virtual TblPermiso IdPermisoNavigation { get; set; } = null!;

    public virtual TblRol IdRolNavigation { get; set; } = null!;
}
