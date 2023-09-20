using System;
using System.Collections.Generic;

namespace EncuestasAPI2.Models;

public partial class Respuestum
{
    public int Id { get; set; }

    public string? Respuesta { get; set; }

    public int? IdPregunta { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Preguntum? IdPreguntaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
