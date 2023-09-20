using System;
using System.Collections.Generic;

namespace EncuestasAPI2.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string? NumeroTelefono { get; set; }

    public string? Nss { get; set; }

    public string Correo { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int IdTipoUsuario { get; set; }

    public int IdRegion { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public virtual Region IdRegionNavigation { get; set; } = null!;

    public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Respuestum> Respuesta { get; set; } = new List<Respuestum>();
}
