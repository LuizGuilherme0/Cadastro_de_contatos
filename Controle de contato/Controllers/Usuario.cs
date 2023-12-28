using Microsoft.AspNetCore.Mvc;

namespace Controle_de_contato.Controllers
{
    public class Usuario : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
