using HomeFinance.Infra.DTOs.Response;
using HomeFinance.Infra.DTOs.Request;

namespace HomeFinance.Infra.Interfaces
{
    public interface IIdentityService
    {
        Task<UsuarioCadastroResponse> CadastrarUsuario(UsuarioCadastroRequest usuarioCadastro);
        Task<UsuarioLoginResponse> LoginUsuario(UsuarioLoginRequest usuarioLogin);
        Task LogOut();
    }
}
