using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class TblUsuario
{
    public int IdUsuario { get; set; }

    public int IdRol { get; set; }

    public string PrimerNombre { get; set; } = null!;

    public string SegundoNombre { get; set; } = null!;

    public string PrimerApellido { get; set; } = null!;

    public string SegundoApellido { get; set; } = null!;

    public string? Email { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string ClaveAcceso { get; set; } = null!;

    public int? Telefono { get; set; }

    public bool? Estado { get; set; }

    public virtual TblRol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<TblCompra> TblCompras { get; set; } = new List<TblCompra>();

    public virtual ICollection<TblHistorialRefreshToken> TblHistorialRefreshTokens { get; set; } = new List<TblHistorialRefreshToken>();

    public virtual ICollection<TblVenta> TblVenta { get; set; } = new List<TblVenta>();
}
