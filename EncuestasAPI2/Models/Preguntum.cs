using System;
using System.Collections.Generic;

namespace EncuestasAPI2.Models;

public partial class Preguntum
{
    public int Id { get; set; }

    public string Pregunta { get; set; } = null!;

    public int IdTipoPregunta { get; set; }

    public int IdCategoria { get; set; }

    public int IdEncuestas { get; set; }

    public virtual CategoriaPreguntum IdCategoriaNavigation { get; set; } = null!;

    public virtual Encuestum IdEncuestasNavigation { get; set; } = null!;

    public virtual TipoPreguntum IdTipoPreguntaNavigation { get; set; } = null!;

    public virtual ICollection<Respuestum> Respuesta { get; set; } = new List<Respuestum>();
}
