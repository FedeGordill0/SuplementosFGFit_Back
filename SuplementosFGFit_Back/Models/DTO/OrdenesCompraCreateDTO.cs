using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models;

public partial class OrdenesCompraCreateDTO
{
    public DateTime? FechaRegistro { get; set; }

    public int? IdEstadoOrden { get; set; }

    public int? IdFormaEnvio { get; set; }

    public int? IdFormaPago { get; set; }

    public int? IdProveedor { get; set; }

}
