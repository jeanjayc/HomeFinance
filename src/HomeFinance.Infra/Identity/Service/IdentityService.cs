using HomeFinance.Infra.DTOs.Request;
using HomeFinance.Infra.DTOs.Response;
using HomeFinance.Infra.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HomeFinance.Infra.Identity.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<UsuarioCadastroResponse> CadastrarUsuario(UsuarioCadastroRequest usuarioCadastro)
        {
            var identityUser = new IdentityUser
            {
                UserName = usuarioCadastro.Email,
                Email = usuarioCadastro.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(identityUser, usuarioCadastro.Senha);

            if(result.Succeeded)
            {
                await _userManager.SetLockoutEnabledAsync(identityUser, false);
            }

            var usuarioCadastroResponse = new UsuarioCadastroResponse(result.Succeeded);

            if(!result.Succeeded && result.Errors.Any())
            {
                usuarioCadastroResponse.AdicionarErro(result.Errors.Select(e => e.Description));
            }

            return usuarioCadastroResponse;
        }

        public async Task<UsuarioLoginResponse> LoginUsuario(UsuarioLoginRequest usuarioLogin)
        {
            var user = await _userManager.FindByNameAsync(usuarioLogin.Email);
            var usuarioLoginResponse = new UsuarioLoginResponse(false);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);

                usuarioLoginResponse = new UsuarioLoginResponse(result.Succeeded);

                if (!result.Succeeded)
                {
                    if (result.IsLockedOut)
                    {
                        usuarioLoginResponse.Erros.Add("Usuário bloqueado por tentativas inválidas");
                    }
                    else if (result.IsNotAllowed)
                    {
                        usuarioLoginResponse.Erros.Add("Usuário não está autorizado a acessar o sistema");
                    }
                    else if (result.RequiresTwoFactor)
                    {
                        usuarioLoginResponse.Erros.Add("Usuário ou senha inválidos");
                    }
                    else
                    {
                        usuarioLoginResponse.Erros.Add("Usuário ou senha inválidos");
                    }
                }
            }
            else
            {
                usuarioLoginResponse.Erros.Add("Usuário não encontrado");
            }

            return usuarioLoginResponse;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
