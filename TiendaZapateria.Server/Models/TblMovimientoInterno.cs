using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class TblMovimientoInterno
{
    public int IdMovimientoInterno { get; set; }

    public int IdInventario { get; set; }

    public string TipoMovimiento { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int? Cantidad { get; set; }

    public DateTime Fecha { get; set; }

    public bool? Estado { get; set; }

    public virtual TblInventario IdInventarioNavigation { get; set; } = null!;
}
