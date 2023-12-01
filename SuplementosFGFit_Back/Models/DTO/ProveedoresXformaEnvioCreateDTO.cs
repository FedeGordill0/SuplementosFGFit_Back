using SuplementosFGFit_Back.Models.DTO;
using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models;

public partial class ProveedoresXformaEnvioCreateDTO
{
    public int? IdProveedor { get; set; }

    public int? IdFormaEnvio { get; set; }

    public virtual FormasEnvio? IdFormaEnvioNavigation { get; set; }

    public virtual Proveedore? IdProveedorNavigation { get; set; }
}
