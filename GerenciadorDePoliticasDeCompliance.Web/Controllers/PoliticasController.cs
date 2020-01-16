using GerenciadorDePoliticasDeCompliance.Web.Models.Politicas;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using GerenciadorDePoliticasDeCompliance.Core.BancoDeDados;

namespace GerenciadorDePoliticasDeCompliance.Controllers
{
    public class PoliticasController : Controller
    {
        private Conexao Conexao { get; set; }
        public PoliticasController()
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

            var comandosql = new SqlCommand(Queries.QUERY_CADASTRO_POLITICAS);
            comandosql.Parameters.AddWithValue("@Titulo", modelo.Titulo);
            comandosql.Parameters.AddWithValue("@Texto", modelo.Texto);
            comandosql.Parameters.AddWithValue("@Data", modelo.Data);


            Conexao.ExecutarQuery(comandosql);

            return RedirectToAction("Index");
        }    
    }
}