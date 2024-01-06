using Controle_de_contato.Filters;
using Controle_de_contato.Models;
using Controle_de_contato.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_contato.Controllers
{
    [PaginaRestritaSomenteAdmin]
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
                    TempData["mensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(Usuario);
            }

            catch (Exception erro)
            {
                TempData["mensagemErro"] = $"Ops, não conseguimos cadastrar seu usuário, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

       public IActionResult Editar(int id)
        {
            UsuarioModel Usuario = _UsuarioRepositorio.ListarPorId(id);
            return View(Usuario);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel Usuario= _UsuarioRepositorio.ListarPorId(id);
            return View(Usuario);
        }


        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _UsuarioRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["mensagemSucesso"] = "Usuário apagado com sucesso!";

                }

                else
                {
                    TempData["mensagemErro"] = $"Ops, não conseguimos apagar seu usuário, tente novamente.";
                }

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["mensagemErro"] = $"Ops, não conseguimos apagar seu usuário, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(UsuarioSemSenhaModel UsuarioSemSenhaModel)
        {
            try
            {
                UsuarioModel Usuario = null;
                if (ModelState.IsValid)
                {
                    Usuario = new UsuarioModel()
                    {
                        Id = UsuarioSemSenhaModel.Id,
                        Nome = UsuarioSemSenhaModel.Nome,
                        Email = UsuarioSemSenhaModel.Email,
                        Login = UsuarioSemSenhaModel.Login,
                        Perfil = UsuarioSemSenhaModel.Perfil
                    };

                    Usuario = _UsuarioRepositorio.Atualizar(Usuario);
                    TempData["mensagemSucesso"] = "O Usuário foi alterado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View("Editar", Usuario);
            }

            catch (Exception erro)
            {
                TempData["mensagemErro"] = $"Ops, não conseguimos atualizar seu usuário, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
