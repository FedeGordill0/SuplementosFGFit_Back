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

    public virtual OrdenesCompra? IdOrdenCompraNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
