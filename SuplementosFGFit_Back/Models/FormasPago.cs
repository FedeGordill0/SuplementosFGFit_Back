using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models;

public partial class FormasPago
{
    public int IdFormaPago { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool? Estado { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Porcentaje { get; set; }

    public virtual ICollection<OrdenesCompra> OrdenesCompras { get; } = new List<OrdenesCompra>();

    public virtual ICollection<ProveedoresXformaPago> ProveedoresXformaPagos { get; } = new List<ProveedoresXformaPago>();
}
