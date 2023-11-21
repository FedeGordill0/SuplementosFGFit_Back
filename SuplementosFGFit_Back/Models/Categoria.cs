using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
