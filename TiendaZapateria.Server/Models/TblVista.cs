using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class TblVista
{
    public int IdVista { get; set; }

    public int IdRol { get; set; }

    public string? UrlName { get; set; }

    public bool? Estado { get; set; }

    public virtual TblRol IdRolNavigation { get; set; } = null!;
}
