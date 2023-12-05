using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaZapateria.Shared
{
    public class CategoriaDTO
    {
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string NombreCategoria { get; set; } = null!;

        [Required(ErrorMessage = "El campo Descripción es obligatorio")]
        public string Descripcion { get; set; } = null!;
        public override bool Equals(object o)
        {
            var other = o as CategoriaDTO;
            return other?.IdCategoria == IdCategoria;
        }
        public override int GetHashCode() => IdCategoria.GetHashCode();
        public override string ToString()
        {
            return NombreCategoria;
        }
    }
}
