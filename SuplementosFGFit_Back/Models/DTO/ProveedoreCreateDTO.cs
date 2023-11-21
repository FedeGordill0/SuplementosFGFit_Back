using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models.DTO;

public partial class ProveedoreCreateDTO
{
    public int IdProveedor { get; set; }
    public List<int> FormasEnvioIds { get; set; } = null!;
    public List<int> FormasPagoIds { get; set; } = null!;
    public List<ProductoPrecioDTO> ProductosIds { get; set; } = null!;
    public string Cuit { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? Estado { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public virtual ICollection<OrdenesCompra> OrdenesCompras { get; } = new List<OrdenesCompra>();

    public virtual ICollection<ProductosXproveedore> ProductosXproveedores { get; } = new List<ProductosXproveedore>();

    public virtual ICollection<ProveedoresXformaEnvio> ProveedoresXformaEnvios { get; } = new List<ProveedoresXformaEnvio>();

    public virtual ICollection<ProveedoresXformaPago> ProveedoresXformaPagos { get; } = new List<ProveedoresXformaPago>();

    public List<ProductoPrecioDTO> ProductosPrecios { get; set; }
}
public class ProductoPrecioDTO
{
    public int IdProducto { get; set; }
    public decimal Precio { get; set; }
}