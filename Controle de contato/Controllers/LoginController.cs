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

        private IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email) 
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }
        public IActionResult Index()
        {
            // Se o usuário estiver logado, redirecionar para a home
            if(_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult RedefinirSenha()
        {
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

        [HttpPost] 
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel Usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (Usuario != null)
                    {
                        string novaSenha = Usuario.GerarNovaSenha();
                        string mensagem = $"Sua nova senha é: {novaSenha}";

                        bool emailEnviado = _email.Enviar(Usuario.Email, "Sistema de Contatos - Nova Senha.", mensagem);

                        if (emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(Usuario);
                            TempData["mensagemSucesso"] = $"Enviamos para seu e-mail cadastrado uma nova senha.";
                        }
                        else
                        {
                            TempData["mensagemErro"] = $"Não conseguimos enviar e-mail. Por favor, tente novamente.";
                        }
       
                        return RedirectToAction("Index", "Login");
                    }

                    TempData["mensagemErro"] = $"Não conseguimos redefinir sua senha. Por favor, verifique os dados informados.";
                }

                return View("Index");
            }

            catch (Exception erro)
            {
                TempData["mensagemErro"] = $"Ops, não conseguimos redefinir sua senha, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }

}
