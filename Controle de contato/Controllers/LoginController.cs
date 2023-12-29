using Controle_de_contato.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Controle_de_contato.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(loginModel.Login =="adm" && loginModel.senha =="123")
                    return RedirectToAction("Index", "Home");
                }
                TempData["mensagemErro"] = $"login e/ou senha Inválida(s). Por favor, tente novamente.";

                return View("Index");
            }

            catch (Exception erro) {
                TempData["mensagemErro"] = $"Ops, não conseguimos realizar o seu login, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
