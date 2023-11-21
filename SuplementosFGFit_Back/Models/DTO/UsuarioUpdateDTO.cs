using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models.DTO;

public partial class UsuarioUpdateDTO
{
    public int IdUsuario { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? IdRol { get; set; }

    public virtual Role? IdRolNavigation { get; set; }
}
