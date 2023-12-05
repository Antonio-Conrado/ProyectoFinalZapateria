using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class TblRol
{
    public int IdRol { get; set; }

    public string? NombreRol { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<TblRolPermiso> TblRolPermisos { get; set; } = new List<TblRolPermiso>();

    public virtual ICollection<TblUsuario> TblUsuarios { get; set; } = new List<TblUsuario>();

    public virtual ICollection<TblVista> TblVista { get; set; } = new List<TblVista>();
}
