using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class CatMunicipio
{
    public int IdMunicipio { get; set; }

    public int IdDepartamento { get; set; }

    public string NombreMunicipio { get; set; } = null!;

    public bool? Estado { get; set; }

    public virtual ICollection<CatProveedor> CatProveedors { get; set; } = new List<CatProveedor>();

    public virtual CatDepartamento IdDepartamentoNavigation { get; set; } = null!;
}
