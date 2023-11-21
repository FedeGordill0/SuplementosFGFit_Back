using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models;

public partial class EstadoOrdenCompraCreateDTO
{
    public string? Estado { get; set; }

    public virtual ICollection<OrdenesCompra> OrdenesCompras { get; } = new List<OrdenesCompra>();
}
