using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models.DTO;

public partial class UsuarioCreateDTO
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? IdRol { get; set; }

}
