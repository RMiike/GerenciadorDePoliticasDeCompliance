using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
        public UsuarioController(UserManager<Usuario> gerenciadorUsuario)
        {
            _gerenciadorUsuario = gerenciadorUsuario;
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
                    var resultado = await _gerenciadorUsuario.CreateAsync(usuario, modelo.SenhaHash);
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
                var usuario = await _gerenciadorUsuario.FindByNameAsync(modelo.Email);

                if (usuario != null && await _gerenciadorUsuario.CheckPasswordAsync(usuario, modelo.SenhaHash))
                {
                    var identidade = new ClaimsIdentity(IdentityConstants.ApplicationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    identidade.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id));
                    identidade.AddClaim(new Claim(ClaimTypes.Name, usuario.Email));
                    identidade.AddClaim(new Claim(ClaimTypes.Role, usuario.Perfil == Perfil.Administrador ? "Administrador" : "Funcionario"));

                    await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identidade), new AuthenticationProperties { IsPersistent = false });

                    return RedirectToAction("Index", "Politica");
                }
                {
                    ModelState.AddModelError("", "Usuário ou senha inválidos.");
                }
            }
            return View();
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
                if(usuario != null)
                {
                    var token = await _gerenciadorUsuario.GeneratePasswordResetTokenAsync(usuario);
                    var urlReset = Url.Action("ResetarSenha", "Usuario", new { token = token, email = modelo.Email }, Request.Scheme);

                    System.IO.File.WriteAllText("linkDeReset.txt", urlReset);

                    TempData["EsqueciSenha"] = "Email enviado";
                    return View("Confirmacao");
                }
                TempData["EsqueciSenha"] = "Email enviado";
                return View("Confirmacao");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ResetarSenha()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetarSenha(ResetarSenhaViewModel modelo)
        {
            return View();
        }

    }
}