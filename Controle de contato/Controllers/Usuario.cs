using Controle_de_contato.Models;
using Controle_de_contato.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_contato.Controllers
{
    public class Usuario : Controller
    {
        private readonly IUsuarioRepositorio _UsuarioRepositorio;
        public Usuario(IUsuarioRepositorio UsuarioRepositorio)
        {
            _UsuarioRepositorio = UsuarioRepositorio;
        }

        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _UsuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel Usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Usuario = _UsuarioRepositorio.Adicionar(Usuario);
                    TempData["mensagemSucesso"] = "Usuario cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(Usuario);
            }

            catch (Exception erro)
            {
                TempData["mensagemErro"] = $"Ops, não conseguimos cadastrar seu Usuario, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
