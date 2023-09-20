using System;
using System.Collections.Generic;

namespace EncuestasAPI2.Models;

public partial class TipoUsuario
{
    public int Id { get; set; }

    public string Tipo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
