using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class TblDetalleInventario
{
    public int IdDetalleInventario { get; set; }

    public int IdProducto { get; set; }

    public int IdCategoria { get; set; }

    public int IdMarca { get; set; }

    public int IdColor { get; set; }

    public int IdTalla { get; set; }

    public int IdMaterial { get; set; }

    public virtual CatCategoria IdCategoriaNavigation { get; set; } = null!;

    public virtual CatColor IdColorNavigation { get; set; } = null!;

    public virtual CatMarca IdMarcaNavigation { get; set; } = null!;

    public virtual CatMaterial IdMaterialNavigation { get; set; } = null!;

    public virtual CatProducto IdProductoNavigation { get; set; } = null!;

    public virtual CatTalla IdTallaNavigation { get; set; } = null!;

    public virtual ICollection<TblInventario> TblInventarios { get; set; } = new List<TblInventario>();
}
