using System;
using System.Collections.Generic;

namespace TiendaZapateria.Server.Models;

public partial class CatProveedor
{
    public int IdProveedor { get; set; }

    public int IdDepartamento { get; set; }

    public int IdMunicipio { get; set; }

    public string NombreProveedor { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public int Telefono { get; set; }

    public string? Correo { get; set; }

    public bool? Estado { get; set; }

    public virtual CatDepartamento IdDepartamentoNavigation { get; set; } = null!;

    public virtual CatMunicipio IdMunicipioNavigation { get; set; } = null!;

    public virtual ICollection<TblCompra> TblCompras { get; set; } = new List<TblCompra>();
}
