using HomeFinance.Infra.DTOs.Request;
using HomeFinance.Infra.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityService _service;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IIdentityService service, ILogger<AccountController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new UsuarioLoginRequest { ReturnUrl = returnUrl});
        }

        [HttpPost] 
        public async Task<IActionResult> Login(UsuarioLoginRequest usuarioLogin)
        {
            try
            {
                var result = await _service.LoginUsuario(usuarioLogin);

                if (!result.Sucesso)
                {
                    foreach (var erro in result.Erros)
                    {
                        ModelState.AddModelError(string.Empty, erro);
                    }

                    return View(usuarioLogin);
                }

                return RedirectToAction("Index", "Finances");

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UsuarioCadastroRequest usuarioCadastro)
        {
            if(!ModelState.IsValid)
            {
                return View(usuarioCadastro);
            }

            var cadastro = await _service.CadastrarUsuario(usuarioCadastro);

            if(!cadastro.Sucesso)
            {
                foreach(var erro in cadastro.Erros)
                {
                    ModelState.AddModelError(string.Empty, erro);
                }

                return View(usuarioCadastro);
            }

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await _service.LogOut();
            return RedirectToAction("Login");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
