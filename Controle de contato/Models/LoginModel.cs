using System.ComponentModel.DataAnnotations;

namespace Controle_de_contato.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite o login")]
        public string Login {  get; set; }

        [Required(ErrorMessage = "Digite a senha")]
        public string senha { get; set; }
    }
}
