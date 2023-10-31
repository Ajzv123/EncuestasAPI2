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
    [Route("api/region")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly ILogger<RegionController> _logger;
        private readonly EncuestasAppContext _db;

        public RegionController(ILogger<RegionController> logger, EncuestasAppContext db)
        {
            _logger = logger;
            _db = db;
        }

        

        //GET: api/regionByIDSP/5
        [HttpGet("regionByIDSP/")]
        public async Task<ActionResult> GetRegionAsyncSP(int id)
        {
            try
            {
                List<object> region = new List<object>();

                SqlConnection conexion = (SqlConnection)_db.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sps_getRegionByID";
                comando.Parameters.Add("@RegionID", System.Data.SqlDbType.Int).Value = id;
                SqlDataReader reader = await Task.Run(() => comando.ExecuteReader());

                while (reader.Read())
                {
                    region.Add(new
                    {
                        Nombre = (string)reader["NombreRegion"],
                    });
                }
                conexion.Close();

                return Ok(region);
            }
            catch
            {
                return BadRequest("Error");
            }
        }

        //POST: api/InsertRegionSP/5
        [HttpPost("~/api/InsertRegionSP")]
        public async Task<ActionResult> InsertRegionAsyncSP([FromBody] RegionDTO region)
        {
            try
            {
                List<object> response = new List<object>();

                SqlConnection conexion = (SqlConnection)_db.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sps_insertRegion";
                comando.Parameters.Add("@NombreRegion", System.Data.SqlDbType.VarChar,50).Value = region.NombreRegion;
                SqlDataReader reader = await Task.Run(() => comando.ExecuteReader());

            }
            catch
            {
                return BadRequest("Error");
            }
            return Ok("Region agregada");
        }

        //PUT : api/UpdateRegionSPById/5 
        [HttpPut("~/api/UpdateRegionSPById")]
        public async Task<ActionResult> UpdateRegionAsyncSP([FromBody] RegionDTO region)
        {
            try
            {
                List<object> response = new List<object>();

                SqlConnection conexion = (SqlConnection)_db.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sps_updateRegionById";
                comando.Parameters.Add("@RegionId", System.Data.SqlDbType.Int).Value = region.Id;
                comando.Parameters.Add("@NuevoNombre", System.Data.SqlDbType.VarChar, 50).Value = region.NombreRegion;
                
                SqlDataReader reader = await Task.Run(() => comando.ExecuteReader());

            }
            catch
            {
                return BadRequest("Error, No se pudo actualizar, contacte a su administrador (si es el administrador, rece[anotación, no funciono rezar])");
            }
            return Ok("Region " +region.Id+" "+region.NombreRegion+" "+ "  actualizada");
        }

        //DELETE: api/DeleteRegionSPById/5
        [HttpDelete("~/api/DeleteRegionSPById")]
        public async Task<ActionResult> DeleteRegionAsyncSP(int id)
        {
            try
            {
                List<object> response = new List<object>();

                SqlConnection conexion = (SqlConnection)_db.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sps_deleteRegionById";
                comando.Parameters.Add("@RegionId", System.Data.SqlDbType.Int).Value = id;
                SqlDataReader reader = await Task.Run(() => comando.ExecuteReader());

            }
            catch
            {
                return BadRequest("Error, No se pudo eliminar, contacte a su administrador (si es el administrador, rece[anotación, no funciono rezar])");
            }
            return Ok("Region " +id+ " eliminada con exito");
        }
        
       
        

    }
}
