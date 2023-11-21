using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models.DTO;

public partial class ProveedoreUpdateDTO
{
    public int IdProveedor { get; set; }

    public string Cuit { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? Estado { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

  //  public virtual ICollection<OrdenesCompra> OrdenesCompras { get; } = new List<OrdenesCompra>();

  //  public List<ProductosXproveedore> ProductosXproveedores { get; set; }
  //public List<ProveedoresXformaEnvio> ProveedoresXformaEnvios { get; set; } = new List<ProveedoresXformaEnvio>();
  //  public List<ProveedoresXformaPago> ProveedoresXformaPagos { get; set; }
  //  public List<ProductoPrecioDTO> ProductosPrecios { get; set; }
}