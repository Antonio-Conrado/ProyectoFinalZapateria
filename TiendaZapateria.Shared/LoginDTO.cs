using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaZapateria.Shared
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Ingrese el correo")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingrese la contraseña")]
        public string ClaveAcceso { get; set; } = null!;
    }
}
