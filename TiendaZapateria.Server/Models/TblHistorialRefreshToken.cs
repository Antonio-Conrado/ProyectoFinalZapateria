using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class TblHistorialRefreshToken
{
    public int IdHistorialToken { get; set; }

    public int? IdUsuario { get; set; }

    public string? Token { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public bool? EsActivo { get; set; }

    public virtual TblUsuario? IdUsuarioNavigation { get; set; }
}
