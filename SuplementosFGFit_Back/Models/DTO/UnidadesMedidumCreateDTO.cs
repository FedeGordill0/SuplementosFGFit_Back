﻿using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models.DTO;

public partial class UnidadesMedidumCreateDTO
{
    public string Descripcion { get; set; } = null!;

    public bool? Estado { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
