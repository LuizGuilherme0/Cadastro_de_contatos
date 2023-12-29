using Controle_de_contato.Enums;
using System.ComponentModel.DataAnnotations;

namespace Controle_de_contato.Models
{
    public class UsuarioSemSenhaModel
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

        
    }
}
