using Controle_de_contato.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_contato.Controllers
{
    [PaginaParaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
