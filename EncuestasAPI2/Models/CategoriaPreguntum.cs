using System;
using System.Collections.Generic;

namespace EncuestasAPI2.Models;

public partial class CategoriaPreguntum
{
    public int Id { get; set; }

    public string Categoria { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Preguntum> Pregunta { get; set; } = new List<Preguntum>();
}
