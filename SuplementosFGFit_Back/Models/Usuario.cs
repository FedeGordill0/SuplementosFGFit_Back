using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? IdRol { get; set; }

    public virtual ICollection<HistorialRefreshToken> HistorialRefreshTokens { get; } = new List<HistorialRefreshToken>();

    public virtual Role? IdRolNavigation { get; set; }

    public virtual ICollection<UsuariosXrole> UsuariosXroles { get; } = new List<UsuariosXrole>();
}
