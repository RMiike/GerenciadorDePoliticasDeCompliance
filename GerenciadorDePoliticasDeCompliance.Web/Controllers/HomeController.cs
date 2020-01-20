using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GerenciadorDePoliticasDeCompliance.Models;
using Microsoft.AspNetCore.Authorization;
using GerenciadorDePoliticasDeCompliance.Web.Models.Politicas;
using System.Data.SqlClient;
using GerenciadorDePoliticasDeCompliance.Core.BancoDeDados;

namespace GerenciadorDePoliticasDeCompliance.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Conexao Conexao { get; set; }


 
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            Conexao = new Conexao();
        }

        public IActionResult Index()
        {
            ListaViewModel lista = new ListaViewModel();

            var comandosql = new SqlCommand(Queries.QUERY_LISTAR_POLITICA);
            var dataReader = Conexao.Consultar(comandosql);
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    var id = int.Parse(dataReader["Id"].ToString());
                    var titulo = dataReader["Titulo"].ToString();
                    var data = DateTime.Parse(dataReader["Data"].ToString());

                    lista.Politicas.Add(new PoliticaDaListaViewModel(id, titulo, data.ToString("dd/MM/yyyy")));
                }
            }
            return View(lista);
        }

        public IActionResult Detalhes(int id)
        {
            ListaViewModel detalhes = new ListaViewModel();

            var comandosql = new SqlCommand(Queries.QUERY_LISTAR_ID_POLITICA);
            comandosql.Parameters.AddWithValue("@Id", id);

            var dataReader = Conexao.Consultar(comandosql);

            dataReader.Read();
            var titulo = dataReader["Titulo"].ToString();
            var texto = dataReader["Texto"].ToString();

            detalhes.Politicas.Add(new PoliticaDaListaViewModel(titulo, texto, id));


            return View(detalhes);
        }

      /*TODO  public IActionResult Assinar(Lis)
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
                    ListaViewModel detalhes = new ListaViewModel();

            var comandosql = new SqlCommand(Queries.QUERY_LISTAR_ID_POLITICA);
            comandosql.Parameters.AddWithValue("@Id", id);

            var dataReader = Conexao.Consultar(comandosql);

            dataReader.Read();
            var titulo = dataReader["Titulo"].ToString();
            var texto = dataReader["Texto"].ToString();

            detalhes.Politicas.Add(new PoliticaDaListaViewModel(titulo, texto, id));


            return View(detalhes);

        }*/


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
