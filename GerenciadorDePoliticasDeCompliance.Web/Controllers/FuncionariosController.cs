using GerenciadorDePoliticasDeCompliance.Core.BancoDeDados;
using GerenciadorDePoliticasDeCompliance.Core.Dominio;
using GerenciadorDePoliticasDeCompliance.Models.Funcionarios;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace GerenciadorDePoliticasDeCompliance.Controllers
{
    public class FuncionariosController : Controller
    {
        private Conexao Conexao { get; set; }

        public FuncionariosController()
        {
            Conexao = new Conexao();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Formulario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(FormularioViewModel modelo)
        {
            Funcionario funcionario = modelo.ConverterParaFuncionario();
            Usuario usuario = modelo.ConverterParaUsuario();

           

            var comandosql = new SqlCommand(Queries.QUERY_CADASTRO_USUARIO);
            comandosql.Parameters.AddWithValue("@Email", usuario.Email);
            comandosql.Parameters.AddWithValue("@Senha", usuario.Senha);
            comandosql.Parameters.AddWithValue("@IdPerfil", (int)usuario.Perfil);
            int idusuario = Conexao.ExecutarQuery(comandosql);

            comandosql = new SqlCommand(Queries.QUERY_CADASTRO_FUNCIONARIO);
            comandosql.Parameters.AddWithValue("@Nome", funcionario.Nome);
            comandosql.Parameters.AddWithValue("@CPF", funcionario.CPF);
            comandosql.Parameters.AddWithValue("@Matricula", funcionario.Matricula);
            comandosql.Parameters.AddWithValue("@Email", funcionario.Email);
            comandosql.Parameters.AddWithValue("@IdUsuario", idusuario);
            Conexao.ExecutarQuery(comandosql);


            return RedirectToAction("Index");
        }
    }
}