using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class CatDepartamento
{
    public int IdDepartamento { get; set; }

    public string NombreDepartamento { get; set; } = null!;

    public bool? Estado { get; set; }

    public virtual ICollection<CatMunicipio> CatMunicipios { get; set; } = new List<CatMunicipio>();

    public virtual ICollection<CatProveedor> CatProveedors { get; set; } = new List<CatProveedor>();
}
