using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaZapateria.Shared
{
    public class RolDTO
    {
        public int IdRol { get; set; }

        [Required(ErrorMessage = "Ingrese el Rol")]
        public string NombreRol { get; set; } = null!;
        public bool? Estado { get; set; } = true;

        public override bool Equals(object o)
        {
            var other = o as RolDTO;
            return other?.IdRol == IdRol;
        }
        public override int GetHashCode() => IdRol.GetHashCode();
        public override string ToString()
        {
            return NombreRol;
        }

        
        //public virtual ICollection<VistaDTO> TblVista { get; set; } = new List<VistaDTO>();
    }
}
