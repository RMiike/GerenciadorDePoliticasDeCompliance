using GerenciadorDePoliticasDeCompliance.Core.BancoDeDados;
using GerenciadorDePoliticasDeCompliance.Core.Dominio;
using GerenciadorDePoliticasDeCompliance.Web.Models.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GerenciadorDePoliticasDeCompliance.Controllers
{
    public class LoginController : Controller
    {
        private Conexao Conexao { get; set; }

        public LoginController()
        {
            Conexao = new Conexao();
        }

        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }



        [HttpPost]
        public async Task<IActionResult> Acessar(LoginViewModel modelo)
        {

            var comandosql = new SqlCommand(Queries.QUERY_AUTENTICAR_USUARIO);

            comandosql.Parameters.AddWithValue("@Email", modelo.Email);

            var dataReader = Conexao.Consultar(comandosql);
            if (dataReader.HasRows)
            {

                comandosql.Parameters.AddWithValue("@Email", modelo.Email);
                dataReader.Read();
                var id = int.Parse(dataReader["Id"].ToString());
                var email = dataReader["Email"].ToString();
                var senha = dataReader["Senha"].ToString();
                var perfil = (PerfilDeUsuario)dataReader["IdPerfil"];

                Usuario usuario = new Usuario(id, perfil, email, senha);

                if (modelo.Senha == usuario.Senha)
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Email, usuario.Email));
                    identity.AddClaim(new Claim(ClaimTypes.Role, usuario.Perfil == PerfilDeUsuario.Administrador ? "Administrador" : "Funcionario"));
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true });
                    if(usuario.Perfil == PerfilDeUsuario.Administrador)
                    { 
                    return RedirectToAction("Index", "Politicas");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {

                    ViewData["UsuarioInvalido"] = "Usuário inválido!";
                }
            }
            else
            {
                ViewData["UsuarioInvalido"] = "Usuário inválido!";
            }
            return View("Index");
        }

        public async Task<IActionResult> Deslogar()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}