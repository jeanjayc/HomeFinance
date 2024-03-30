using System.ComponentModel.DataAnnotations;

namespace HomeFinance.Infra.DTOs.Request
{
    public class UsuarioLoginRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Senha { get; set; }
    }
}
