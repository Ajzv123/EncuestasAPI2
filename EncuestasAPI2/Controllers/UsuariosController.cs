using EncuestasAPI2.DTO;
using EncuestasAPI2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncuestasAPI2.Controllers
{
    [Route("api/Usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly EncuestasAppContext _db;

        public UsuariosController(ILogger<UsuariosController> logger, EncuestasAppContext db)
        {
            _logger = logger;
            _db = db;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            if (_db.Usuarios == null)
            {
                return NotFound();
            }
            //return await _db.Usuarios.ToListAsync();
            return await _db.Usuarios.Select(x => UsuarioDTO.UsuarioToDTO(x)).ToListAsync();
        }

        //// GET: api/UsuariosNoDTO
        [HttpGet]
        [Route("~/api/UsuariosNoDTO")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios2()
        {
            if (_db.Usuarios == null)
            {
                return NotFound();
            }
            return await _db.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            if (_db.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _db.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return UsuarioDTO.UsuarioToDTO(usuario);
        }

        // GET: api/UsuarioSP/5
        [HttpGet]
        [Route("~/api/UsuarioSP")]
        public ActionResult GetUsuarioSP(int id)
        {
            try
            {
                List<object> users = new List<object>();

                SqlConnection conexion = (SqlConnection)_db.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sps_getUsuario";
                comando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new
                    {
                        Nombre = (string)reader["Nombre"],
                        NombreRegion = (string)reader["NombreRegion"],
                        NSS = (string)reader["NSS"],
                    });
                }
                conexion.Close();

                return Ok(users);
            }
            catch
            {
                return BadRequest("Error");
            }
        }

        /*
         * lo que permite que se ejecute de forma asincrónica. También hemos cambiado la declaración de la función para que devuelva una tarea.
         */
        // GET: api/UsuarioAsyncSP/5
        [HttpGet]
        [Route("~/api/UsuarioAsyncSP")]
        public async Task<ActionResult> GetUsuarioAsyncSP(int id)
        {
            try
            {
                List<object> user = new List<object>();

                SqlConnection conexion = (SqlConnection)_db.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sps_getUsuario";
                comando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                SqlDataReader reader = await Task.Run(() => comando.ExecuteReader());

                while (reader.Read())
                {
                    user.Add(new
                    {
                        Nombre = (string)reader["Nombre"],
                        NSS = (string)reader["NSS"],
                        NombreRegion = (string)reader["NombreRegion"],
                    });
                }
                conexion.Close();

                return Ok(user);
            }
            catch
            {
                return BadRequest("Error");
            }
        }
        /*
         * El hilo principal no se bloquea mientras se espera a que se complete la operación de lectura de datos. En lugar de eso, la tarea se ejecuta en segundo plano y el hilo principal puede continuar con otras tareas. 
         */

        // POST: api/InsertUsuarioSP/5
        [HttpPost]
        [Route("~/api/InsertUsuarioSP")]
        public async Task<ActionResult<UsuarioDTO>> InsertUsuarioSP(UsuarioDTO usuarioDTO)
        {
            try
            {
                List<object> response = new List<object>();

                SqlConnection conexion = (SqlConnection)_db.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "spi_insertUsuario";
                comando.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar, 50).Value = usuarioDTO.Nombre;
                comando.Parameters.Add("@apellidoPaterno", System.Data.SqlDbType.VarChar, 50).Value = usuarioDTO.ApellidoPaterno;
                comando.Parameters.Add("@apellidoMaterno", System.Data.SqlDbType.VarChar, 50).Value = usuarioDTO.ApellidoMaterno;
                comando.Parameters.Add("@numeroTelefono", System.Data.SqlDbType.VarChar, 20).Value = usuarioDTO.NumeroTelefono;
                comando.Parameters.Add("@nss", System.Data.SqlDbType.VarChar, 11).Value = usuarioDTO.Nss;
                comando.Parameters.Add("@correo", System.Data.SqlDbType.VarChar, 50).Value = usuarioDTO.Correo;
                comando.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Value = usuarioDTO.Password;
                comando.Parameters.Add("@fechaNacimiento", System.Data.SqlDbType.DateTime).Value = usuarioDTO.FechaNacimiento;
                comando.Parameters.Add("@idTipoUsuario", System.Data.SqlDbType.Int).Value = usuarioDTO.IdTipoUsuario;
                comando.Parameters.Add("@idRegion", System.Data.SqlDbType.Int).Value = usuarioDTO.IdRegion;
                SqlDataReader reader = await Task.Run(() => comando.ExecuteReader());

                while (reader.Read())
                {
                    response.Add(new
                    {
                        tx_resultado = (string)reader["tx_resultado"],
                        nu_resultado = (int)reader["nu_resultado"]
                    });
                }
                conexion.Close();

                return Ok(response);
            }
            catch
            {
                return BadRequest("Error");
            }
        }

        // PUT: api/UpdateUsuarioSP/5
        [HttpPut]
        [Route("~/api/UpdateUsuarioSP")]
        public async Task<ActionResult<UsuarioDTO>> UpdateUsuarioSP(UsuarioDTO usuarioDTO)
        {
            try
            {
                List<object> response = new List<object>();

                SqlConnection conexion = (SqlConnection)_db.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "spu_updateUsuario";
                comando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = usuarioDTO.Id;
                comando.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar, 50).Value = usuarioDTO.Nombre;
                comando.Parameters.Add("@apellidoPaterno", System.Data.SqlDbType.VarChar, 50).Value = usuarioDTO.ApellidoPaterno;
                comando.Parameters.Add("@apellidoMaterno", System.Data.SqlDbType.VarChar, 50).Value = usuarioDTO.ApellidoMaterno;
                comando.Parameters.Add("@numeroTelefono", System.Data.SqlDbType.VarChar, 20).Value = usuarioDTO.NumeroTelefono;
                comando.Parameters.Add("@nss", System.Data.SqlDbType.VarChar, 11).Value = usuarioDTO.Nss;
                comando.Parameters.Add("@correo", System.Data.SqlDbType.VarChar, 50).Value = usuarioDTO.Correo;
                comando.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Value = usuarioDTO.Password;
                comando.Parameters.Add("@fechaNacimiento", System.Data.SqlDbType.DateTime).Value = usuarioDTO.FechaNacimiento;
                comando.Parameters.Add("@idTipoUsuario", System.Data.SqlDbType.Int).Value = usuarioDTO.IdTipoUsuario;
                comando.Parameters.Add("@idRegion", System.Data.SqlDbType.Int).Value = usuarioDTO.IdRegion;
                SqlDataReader reader = await Task.Run(() => comando.ExecuteReader());

                while (reader.Read())
                {
                    response.Add(new
                    {
                        tx_resultado = (string)reader["tx_resultado"],
                        nu_resultado = (int)reader["nu_resultado"]
                    });
                }
                conexion.Close();

                return Ok(response);
            }
            catch
            {
                return BadRequest("Error");
            }
        }

        // DELETE: api/DeleteUsuarioSP/5
        [HttpDelete]
        [Route("~/api/DeleteUsuarioSP")]
        public async Task<ActionResult<UsuarioDTO>> DeleteUsuarioSP(int id)
        {
            try
            {
                List<object> response = new List<object>();

                SqlConnection conexion = (SqlConnection)_db.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "spd_deleteUsuario";
                comando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                SqlDataReader reader = await Task.Run(() => comando.ExecuteReader());

                while (reader.Read())
                {
                    response.Add(new
                    {
                        tx_resultado = (string)reader["tx_resultado"],
                        nu_resultado = (int)reader["nu_resultado"]
                    });
                }
                conexion.Close();

                return Ok(response);
            }
            catch
            {
                return BadRequest("Error");
            }
        }
        

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioDTO usuarioDTO)
        {
            if (id != usuarioDTO.Id)
            {
                return BadRequest();
            }

            //_context.Entry(todoItem).State = EntityState.Modified;
            var usuario = await _db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound(id);
            }

            usuario.Nombre = usuarioDTO.Nombre;
            usuario.ApellidoPaterno = usuarioDTO.ApellidoPaterno;
            usuario.ApellidoMaterno = usuarioDTO.ApellidoMaterno;
            usuario.NumeroTelefono = usuarioDTO.NumeroTelefono;
            usuario.Nss = usuarioDTO.Nss;
            usuario.Correo = usuarioDTO.Correo;
            usuario.Password = usuarioDTO.Password;
            usuario.FechaNacimiento = usuarioDTO.FechaNacimiento;
            usuario.IdTipoUsuario = usuarioDTO.IdTipoUsuario;
            usuario.IdRegion = usuarioDTO.IdRegion;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!UsuarioExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> PostUsuario(UsuarioDTO usuarioDTO)
        {
            var usuario = new Usuario
            {
                Nombre = usuarioDTO.Nombre,
                ApellidoPaterno = usuarioDTO.ApellidoPaterno,
                ApellidoMaterno = usuarioDTO.ApellidoMaterno,
                NumeroTelefono = usuarioDTO.NumeroTelefono,
                Nss = usuarioDTO.Nss,
                Correo = usuarioDTO.Correo,
                Password = usuarioDTO.Password,
                FechaNacimiento = usuarioDTO.FechaNacimiento,
                IdTipoUsuario = usuarioDTO.IdTipoUsuario,
                IdRegion = usuarioDTO.IdRegion
            };

            if (_db.Usuarios == null)
            {
                return Problem("Entity set 'EncuestasDBContext.Usuario'  is null.");
            }
            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, UsuarioDTO.UsuarioToDTO(usuario));
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (_db.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _db.Usuarios.Remove(usuario);
            await _db.SaveChangesAsync();

            return NoContent();
        }
        //

        private bool UsuarioExists(int id)
        {
            return (_db.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

