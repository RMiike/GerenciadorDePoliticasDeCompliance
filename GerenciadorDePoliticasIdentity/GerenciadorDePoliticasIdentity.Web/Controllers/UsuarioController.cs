using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GerenciadorDePoliticasIdentity.Core.Data;
using GerenciadorDePoliticasIdentity.Core.Dominio;
using GerenciadorDePoliticasIdentity.Web.Models;
using GerenciadorDePoliticasIdentity.Web.Models.UsuarioModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDePoliticasIdentity.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<Usuario> _gerenciadorUsuario;
        private readonly ServicoPessoa _servicoPessoa;

        public UsuarioController(
            UserManager<Usuario> gerenciadorUsuario,
            ServicoPessoa  servicoPessoa)
        {
            _gerenciadorUsuario = gerenciadorUsuario;
            _servicoPessoa = servicoPessoa;
        }
        public ClaimsPrincipal S2FA(string userId, string provides)
        {
            var identidade = new ClaimsIdentity(new List<Claim>
            {
                 new Claim ("primeiro", userId),
                 new Claim ("segundo", provides)
            }, IdentityConstants.TwoFactorUserIdScheme);
            return new ClaimsPrincipal(identidade);
        }
        [HttpGet]   
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Cadastrar(CadastroViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _gerenciadorUsuario.FindByNameAsync(modelo.Email);
                if (usuario == null)
                {
                    usuario = new Usuario
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = modelo.Email,
                        Telefone = modelo.Telefone,
                        Perfil = Perfil.Funcionario,
                        DoisFatoresDeAutenticacao = modelo.DoisFatoresDeAutenticacao
                    };

                    var pessoa = new Pessoas
                    {
                        Id = Guid.NewGuid().ToString(),
                        Nome = modelo.Nome,
                        CPF = modelo.CPF,
                        IdUsuario = usuario.Id
                    };

                    var resultado = await _gerenciadorUsuario.CreateAsync(usuario, modelo.SenhaHash);
                    _servicoPessoa.Inserir(pessoa);

                    if (resultado.Succeeded)
                    {
                        var token = await _gerenciadorUsuario.GenerateEmailConfirmationTokenAsync(usuario);
                        var confirmacao = Url.Action("ConfirmarEmail", "Usuario", new { Token = token, Email = usuario.Email }, Request.Scheme);
                        System.IO.File.WriteAllText("ConfirmarEmail.Txt", "Clique no lique para confirmar o email " + confirmacao);
                    }
                    if (resultado.Succeeded)
                    {
                        TempData["CadastroRealizadoComSUcesso"] = "Cadastrado com sucesso!";
                        return View("Confirmacao");
                    }
                    else
                    {
                        foreach (var error in resultado.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View();
                    }
                }
                ModelState.AddModelError("", "Usuário já cadastrado.");
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _gerenciadorUsuario.FindByEmailAsync(modelo.Email);
                if (usuario != null && await _gerenciadorUsuario.CheckPasswordAsync(usuario, modelo.SenhaHash))
                {
                    if (!await _gerenciadorUsuario.IsEmailConfirmedAsync(usuario))
                    {
                        ModelState.AddModelError("", "Email não confirmado!!");
                        return View();
                    }

                    if (await _gerenciadorUsuario.GetTwoFactorEnabledAsync(usuario))
                    {
                        var validar = await _gerenciadorUsuario.GetValidTwoFactorProvidersAsync(usuario);
                        if (validar.Contains("Email"))
                        {
                            var token = await _gerenciadorUsuario.GenerateTwoFactorTokenAsync(usuario, "Email");
                            System.IO.File.WriteAllText("email2f.txt", token);
                            await HttpContext.SignInAsync(IdentityConstants.TwoFactorUserIdScheme, S2FA(usuario.Id, "Email"));
                            return RedirectToAction("DoisFatores");
                        }
                    }
                    else
                    {

                        var identidade = new ClaimsIdentity(IdentityConstants.ApplicationScheme, ClaimTypes.Name, ClaimTypes.Role);
                        identidade.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id));
                        identidade.AddClaim((new Claim(ClaimTypes.Name, usuario.Email)));
                        identidade.AddClaim(new Claim(ClaimTypes.Role, usuario.Perfil == Perfil.Administrador ? "Administrador" : "Comum"));

                        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identidade), new AuthenticationProperties { IsPersistent = false });


                        return View("Index");
                    }
                }
                ModelState.AddModelError("", "Usuário ou Senha Invalida");
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult EsqueciSenha()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EsqueciSenha(EsqueciSenhaViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _gerenciadorUsuario.FindByEmailAsync(modelo.Email);
                if (usuario != null)
                {
                    var token = await _gerenciadorUsuario.GeneratePasswordResetTokenAsync(usuario);
                    var urlReset = Url.Action("ResetarSenha", "Usuario", new { token = token, email = modelo.Email }, Request.Scheme);

                    System.IO.File.WriteAllText("linkDeReset.txt", "Clique no link para resetar a senha " + urlReset);

                    TempData["EsqueciSenha"] = "Email enviado";
                    return View("Confirmacao");
                }
                TempData["EsqueciSenha"] = "Email enviado";
                return View("Confirmacao");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ResetarSenha(string token, string email)
        {
            return View(new ResetarSenhaViewModel { Token = token, Email = email });
        }
        [HttpPost]
        public async Task<IActionResult> ResetarSenha(ResetarSenhaViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _gerenciadorUsuario.FindByEmailAsync(modelo.Email);

                if (usuario != null)
                {
                    var resultado = await _gerenciadorUsuario.ResetPasswordAsync(usuario, modelo.Token, modelo.Senha);
                    if (!resultado.Succeeded)
                    {
                        foreach (var error in resultado.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View();
                    }
                    TempData["ResetarSenha"] = "SenhaResetada";
                    return View("Confirmacao");
                }
            }
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> ConfirmarEmail(string token, string email)
        {
            var usuario = await _gerenciadorUsuario.FindByEmailAsync(email);
            if (usuario != null)
            {
                var resultado = await _gerenciadorUsuario.ConfirmEmailAsync(usuario, token);

                if (resultado.Succeeded)
                {
                    TempData["ConfirmacaoEMail"] = "Email confirmado.";
                    return View("Confirmacao");
                }

            }
            return View();

        }

        [HttpGet]
        public IActionResult DoisFatores()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DoisFatores(DoisFatoresViewModel modelo)
        {
            var resultado = await HttpContext.AuthenticateAsync(IdentityConstants.TwoFactorUserIdScheme);
            if (!resultado.Succeeded)
            {
                ModelState.AddModelError("", "Seu token expirou.");
                return View();
            }
            if (ModelState.IsValid)
            {
                var usuario = await _gerenciadorUsuario.FindByIdAsync(resultado.Principal.FindFirstValue("primeiro"));
                if(usuario != null)
                {
                    var valido = await _gerenciadorUsuario.VerifyTwoFactorTokenAsync(usuario, resultado.Principal.FindFirstValue("segundo"), modelo.Token);
                    if (valido)
                    {
                        await HttpContext.SignOutAsync(IdentityConstants.TwoFactorUserIdScheme);

                        var identidade = new ClaimsIdentity(IdentityConstants.TwoFactorUserIdScheme, ClaimTypes.Name, ClaimTypes.Role);
                        identidade.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id));
                        identidade.AddClaim(new Claim(ClaimTypes.Name, usuario.Email));
                        identidade.AddClaim(new Claim(ClaimTypes.Role, usuario.Perfil == Perfil.Administrador ? "Administrador" : "Funcionario"));

                        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identidade), new AuthenticationProperties { IsPersistent = true });

                        return RedirectToAction("Index", "Politica");

                    }
                    ModelState.AddModelError("", "Token Inválido");
                    return View();
                }
                ModelState.AddModelError("", "Inválido");
            }
            return View();
        }
    }
}