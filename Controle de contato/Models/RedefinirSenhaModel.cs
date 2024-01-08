using System.ComponentModel.DataAnnotations;

namespace Controle_de_contato.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "Digite o login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite seu email")]
        public string Email { get; set; }
    }
}
