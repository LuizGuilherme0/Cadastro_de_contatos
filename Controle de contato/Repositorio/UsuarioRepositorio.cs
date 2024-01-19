using Controle_de_contato.Controllers;
using Controle_de_contato.Data;
using Controle_de_contato.Models;

namespace Controle_de_contato.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;  
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            //_bancoContext = bancoContext;
            this._bancoContext = bancoContext;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel BuscarPorEmailELogin(string email, string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
        }
        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios.ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel Usuario)
        {
            // gravar no banco de dados (ejetou o BancoContext com o método construtor)

            Usuario.DataCadastro = DateTime.Now;
            Usuario.SetSenhaHash();
            _bancoContext.Usuarios.Add(Usuario);
            _bancoContext.SaveChanges();

            return Usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel Usuario)
        {
            UsuarioModel UsuarioDB = ListarPorId(Usuario.Id);

            if (UsuarioDB == null) throw new SystemException("Houve um erro na atualização do usuario");
            
            UsuarioDB.Nome = Usuario.Nome;
            UsuarioDB.Login = Usuario.Login;
            UsuarioDB.Email = Usuario.Email;
            UsuarioDB.Perfil = Usuario.Perfil;
            UsuarioDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(UsuarioDB);
            _bancoContext.SaveChanges();

            return UsuarioDB;
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDB = ListarPorId(alterarSenhaModel.Id);

            if (usuarioDB == null) throw new Exception("Houve um erro na atualização da senha, usuário não é encontrado!");

            if (usuarioDB.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere!");

            if (usuarioDB.SenhaValida(alterarSenhaModel.NovaSenha)) throw new Exception("Nova senha deve diferente da senha atual!");

            usuarioDB.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;
        
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDB = ListarPorId(id);

            if (usuarioDB == null) throw new SystemException("Houve um erro na deleção do usuario!");

            _bancoContext.Usuarios.Remove(usuarioDB);
            _bancoContext.SaveChanges();

            return true;

        }

      
    }
}
