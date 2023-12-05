using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaZapateria.Shared
{
    public class DetalleInventarioDTO
    {
        public int IdDetalleInventario { get; set; }

        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }

        public int IdMarca { get; set; }

        public int IdColor { get; set; }

        public int IdTalla { get; set; }

        public int IdMaterial { get; set; }

        public virtual ProductoDTO IdProductoNavigation { get; set; } = null!;
        public virtual CategoriaDTO IdCategoriaNavigation { get; set; } = null!;

        public virtual MarcaDTO IdMarcaNavigation { get; set; } = null!;

        public virtual TallaDTO IdTallaNavigation { get; set; } = null!;

    }
}
