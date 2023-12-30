using Controle_de_contato.Enums;
using System.ComponentModel.DataAnnotations;

namespace Controle_de_contato.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do usuario")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o Login do usuario")]
        public string Login { get; set;}

        [Required(ErrorMessage = "Digite o email do usuario")]
        [EmailAddress(ErrorMessage = "O email informado não é válido!")]
        public string Email{ get; set; }

        [Required(ErrorMessage = "Escolha o perfil do usuario")]
        public PerfilEnum? Perfil { get; set; }

        [Required(ErrorMessage = "Digite a senha do usuario")]
        public string Senha { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAtualizacao{ get; set;}

        public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }
    }
}
