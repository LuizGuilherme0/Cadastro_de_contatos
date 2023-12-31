using Controle_de_contato.Helper;
using Controle_de_contato.Models;
using Controle_de_contato.Repositorio;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Controle_de_contato.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        private readonly ISessao _sessao;
        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao) 
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            // Se o usuário estiver logado, redirecionar para a home
            if(_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel Usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if(Usuario != null)
                    {
                        if (Usuario.SenhaValida(loginModel.senha))
                        {
                            _sessao.CriarSessaoDoUsuario(Usuario);
                            return RedirectToAction("Index", "Home");

                        }

                        TempData["mensagemErro"] = $"Senha Inválida. Por favor, tente novamente.";
                    }

                TempData["mensagemErro"] = $"login e/ou senha Inválida(s). Por favor, tente novamente.";
                }

                return View("Index");
            }

            catch (Exception erro) {
                TempData["mensagemErro"] = $"Ops, não conseguimos realizar o seu login, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
