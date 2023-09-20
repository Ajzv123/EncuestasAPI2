using System;
using System.Collections.Generic;

namespace EncuestasAPI2.Models;

public partial class Region
{
    public int Id { get; set; }

    public string NombreRegion { get; set; } = null!;

    public virtual ICollection<Encuestum> Encuesta { get; set; } = new List<Encuestum>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
