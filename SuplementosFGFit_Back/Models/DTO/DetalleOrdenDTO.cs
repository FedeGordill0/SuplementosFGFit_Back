using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models;

public partial class DetalleOrdenDTO
{
    public int IdDetalle { get; set; }

    public decimal? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public int? IdOrdenCompra { get; set; }

    public int? IdProducto { get; set; }
    public virtual ICollection<OrdenesCompraDTO> IdOrdenCompraNavigation { get; set; } = new List<OrdenesCompraDTO>();


    public virtual Producto? IdProductoNavigation { get; set; }
}
