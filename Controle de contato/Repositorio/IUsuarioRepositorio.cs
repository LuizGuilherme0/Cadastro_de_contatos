using Controle_de_contato.Models;

namespace Controle_de_contato.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorLogin(string login);

        UsuarioModel BuscarPorEmailELogin(string email, string login);
        UsuarioModel ListarPorId(int id);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel Adicionar(UsuarioModel Usuario);
        UsuarioModel Atualizar(UsuarioModel Usuario);
        bool Apagar(int id);
    }
}
