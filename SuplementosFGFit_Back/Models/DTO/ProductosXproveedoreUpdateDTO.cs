using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models;

public partial class ProductosXproveedoreUpdateDTO
{
    public int IdProductoProveedor { get; set; }

    public bool? Estado { get; set; }

    public decimal? Precio { get; set; }

    public int? IdProducto { get; set; }

    public int? IdProveedor { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Proveedore? IdProveedorNavigation { get; set; }
}
