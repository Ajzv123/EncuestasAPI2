using EncuestasAPI2.DTO;
using EncuestasAPI2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EncuestasAPI2.Controllers
{
    [Route("api/Usuarios")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly IL
        public IActionResult Index()
        {
            return View();
        }
    }
}
