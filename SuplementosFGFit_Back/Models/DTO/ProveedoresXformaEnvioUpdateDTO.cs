using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models;

public partial class ProveedoresXformaEnvioUpdateDTO
{
    public int IdProveedorFormaEnvio { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdFormaEnvio { get; set; }

    public virtual FormasEnvio? IdFormaEnvioNavigation { get; set; }

    public virtual Proveedore? IdProveedorNavigation { get; set; }
}
