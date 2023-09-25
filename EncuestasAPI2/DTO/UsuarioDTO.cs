using EncuestasAPI2.Models;

namespace EncuestasAPI2.DTO
{
    public class UsuarioDTO
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

        public static UsuarioDTO UsuarioToDTO(Usuario usuario) =>
            new UsuarioDTO
            {
                Id= usuario.Id,
                Nombre = usuario.Nombre,
                ApellidoPaterno = usuario.ApellidoPaterno,
                ApellidoMaterno = usuario.ApellidoMaterno,
                NumeroTelefono = usuario.NumeroTelefono,
                Nss = usuario.Nss,
                Correo = usuario.Correo,
                Password = usuario.Password,
                IdTipoUsuario = usuario.IdTipoUsuario,
                IdRegion = usuario.IdRegion,
                FechaNacimiento = usuario.FechaNacimiento
            };
    }
}
