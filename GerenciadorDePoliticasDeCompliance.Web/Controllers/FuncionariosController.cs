using GerenciadorDePoliticasDeCompliance.Core.BancoDeDados;
using GerenciadorDePoliticasDeCompliance.Models.Funcionarios;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDePoliticasDeCompliance.Controllers
{
    public class FuncionariosController : Controller
    {
        private Conexao Conexao { get; set; }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Formulario()
        {
            return View();
        }

        [HttpPost]
        public void Cadastrar(FormularioViewModel modelo)
        {
            Conexao.Conectar();
            Conexao.ExecutarQuery("");
            Conexao.Desconectar();
        }
    }
}