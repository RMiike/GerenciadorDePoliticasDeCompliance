using GerenciadorDePoliticasDeCompliance.Web.Models.Politicas;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using GerenciadorDePoliticasDeCompliance.Core.BancoDeDados;
using System;
using GerenciadorDePoliticasDeCompliance.Core.Dominio;
using Microsoft.AspNetCore.Authorization;

namespace GerenciadorDePoliticasDeCompliance.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class PoliticasController : Controller
    {
        private Conexao Conexao { get; set; }
        public PoliticasController()
        {
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

        public IActionResult Formulario()
        {
            return View(new FormularioViewModel());
        }


        [HttpPost]
        public IActionResult Salvar(FormularioViewModel modelo)
        {
            SqlCommand comandosql = null;
            Politica politica = modelo.ConverterParaPolitica();
            if (modelo.Id == 0)
            {
                comandosql = new SqlCommand(Queries.QUERY_CADASTRO_POLITICAS);
                comandosql.Parameters.AddWithValue("@Titulo", politica.Titulo);
                comandosql.Parameters.AddWithValue("@Texto", politica.Texto);
                comandosql.Parameters.AddWithValue("@Data", DateTime.Now);
            }
            else
            {
                comandosql = new SqlCommand(Queries.QUERY_DELETAR_ASSINATURAPOLITICA);
                comandosql.Parameters.AddWithValue("Id", politica.Id);
                Conexao.ExecutarQuery(comandosql);
                Conexao._conexao.Close();
                comandosql = new SqlCommand(Queries.QUERY_ATUALIZAR_POLITICA);
                comandosql.Parameters.AddWithValue("@Id", modelo.Id);
                comandosql.Parameters.AddWithValue("@Titulo", politica.Titulo);
                comandosql.Parameters.AddWithValue("@Texto", politica.Texto);
                comandosql.Parameters.AddWithValue("@Data", DateTime.Now);
            }
            Conexao.ExecutarQuery(comandosql);
            return RedirectToAction("Index");
        }

        public IActionResult Edicao(int id)
        {
            var comandosql = new SqlCommand(Queries.QUERY_LISTAR_ID_POLITICA);
            comandosql.Parameters.AddWithValue("@Id", id);
            var dataReader = Conexao.Consultar(comandosql);
            dataReader.Read();
            var titulo = dataReader["Titulo"].ToString();
            var texto = dataReader["Texto"].ToString();
            FormularioViewModel formulario = new FormularioViewModel(id, titulo, texto);
            return View("Formulario", formulario);
        }

        public IActionResult Deletar(int id)
        {
            var comandosql = new SqlCommand(Queries.QUERY_DELETAR_POLITICA);
            comandosql.Parameters.AddWithValue("@Id", id);
            Conexao.ExecutarQuery(comandosql);
            return RedirectToAction("Index");
        }



    }
}
