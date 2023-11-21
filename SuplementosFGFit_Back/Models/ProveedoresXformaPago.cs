using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models;

public partial class ProveedoresXformaPago
{
    public int IdProveedorFormaPago { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdFormaPago { get; set; }

    public virtual FormasPago? IdFormaPagoNavigation { get; set; }

    public virtual Proveedore? IdProveedorNavigation { get; set; }
}
