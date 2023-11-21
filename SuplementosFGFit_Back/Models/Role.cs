using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models;

public partial class Role
{
    public int IdRol { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();

    public virtual ICollection<UsuariosXrole> UsuariosXroles { get; } = new List<UsuariosXrole>();
}
