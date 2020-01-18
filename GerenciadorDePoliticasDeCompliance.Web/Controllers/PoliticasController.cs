using GerenciadorDePoliticasDeCompliance.Web.Models.Politicas;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using GerenciadorDePoliticasDeCompliance.Core.BancoDeDados;
using System.Data;
using System;
using System.Collections.Generic;
using GerenciadorDePoliticasDeCompliance.Core.Dominio;

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
            DetalheViewModel lista = new DetalheViewModel();

            var comandosql = new SqlCommand(Queries.QUERY_LISTAR_ID_POLITICA + " '" + id + "'");
            var dataReader = Conexao.Consultar(comandosql);
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    var titulo = dataReader["Titulo"].ToString();
                    var texto = dataReader["Texto"].ToString();
                    lista.Politicas.Add(new PoliticaDoDetalheViewModel(id, titulo, texto));
                }
            }
            return View(lista);
        }
        public IActionResult Formulario()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Cadastrar(FormularioViewModel modelo)
        {
            Politica politica = modelo.ConverterParaPolitica();
            var comandosql = new SqlCommand(Queries.QUERY_CADASTRO_POLITICAS);
            comandosql.Parameters.AddWithValue("@Titulo", politica.Titulo);
            comandosql.Parameters.AddWithValue("@Texto", politica.Texto);
            comandosql.Parameters.AddWithValue("@Data", DateTime.Now);


            Conexao.ExecutarQuery(comandosql);

            return RedirectToAction(nameof(Index));
        }

    }
}
