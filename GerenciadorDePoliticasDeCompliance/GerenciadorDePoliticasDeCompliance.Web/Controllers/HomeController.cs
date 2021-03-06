﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GerenciadorDePoliticasDeCompliance.Models;
using Microsoft.AspNetCore.Authorization;
using GerenciadorDePoliticasDeCompliance.Web.Models.Politicas;
using System.Data.SqlClient;
using GerenciadorDePoliticasDeCompliance.Core.BancoDeDados;
using System.Security.Claims;

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
            List<string> listaidpolitica = new List<string>();
            List<string> listapolitica = new List<string>();
            List<string> listapoliticaremovida = new List<string>();

            ListaViewModel listadepoliticasassinadas = new ListaViewModel();
            var claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            var comandosql = new SqlCommand(Queries.QUERY_VERIFICAR_ID_FUNCIONARIO);
            comandosql.Parameters.AddWithValue("@IdUsuario", claim.Value);
            var dataReader = Conexao.Consultar(comandosql);
            dataReader.Read();
            var idfuncionario = int.Parse(dataReader["Id"].ToString());
            Conexao._conexao.Close();
            comandosql = new SqlCommand(Queries.QUERY_LISTAR_IDFUNCIONARIO_ASSINADOS);
            comandosql.Parameters.AddWithValue("IdFuncionario", idfuncionario);
            dataReader = Conexao.Consultar(comandosql);
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    var idpolitica = (dataReader["IdPolitica"].ToString());
                    listaidpolitica.Add(idpolitica);
                }
            }
            Conexao._conexao.Close();//assinadas

            comandosql = new SqlCommand(Queries.QUERY_LISTAR_POLITICA);
            dataReader = Conexao.Consultar(comandosql);
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    var id = (dataReader["Id"].ToString());
                    listapolitica.Add(id);
                    listapoliticaremovida.Add(id);
                }
            }
            foreach (string obj in listapolitica)
            {
                foreach (string obj2 in listaidpolitica)
                {
                    if (obj == obj2)
                    {
                        listapoliticaremovida.Remove(obj);
                    }
                }
            }
            Conexao._conexao.Close();

            foreach (string obj in listapoliticaremovida)
            {
                comandosql = new SqlCommand(Queries.QUERY_LISTAR_ID_POLITICA);
                comandosql.Parameters.AddWithValue("@Id", obj);
                dataReader = Conexao.Consultar(comandosql);
                dataReader.Read();
                var id = int.Parse(dataReader["Id"].ToString());
                var titulo = dataReader["Titulo"].ToString();
                var data = dataReader["Data"].ToString();
                listadepoliticasassinadas.Politicas.Add(new PoliticaDaListaViewModel(id, titulo, data));
                Conexao._conexao.Close();
            }

            return View(listadepoliticasassinadas);

        }

        public IActionResult Assinados()
        {
            List<string> listaidpolitica = new List<string>();
            ListaViewModel listadepoliticasassinadas = new ListaViewModel();
            var claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            var comandosql = new SqlCommand(Queries.QUERY_VERIFICAR_ID_FUNCIONARIO);
            comandosql.Parameters.AddWithValue("@IdUsuario", claim.Value);
            var dataReader = Conexao.Consultar(comandosql);
            dataReader.Read();
            var idfuncionario = int.Parse(dataReader["Id"].ToString());
            Conexao._conexao.Close();
            comandosql = new SqlCommand(Queries.QUERY_LISTAR_IDFUNCIONARIO_ASSINADOS);
            comandosql.Parameters.AddWithValue("IdFuncionario", idfuncionario);
            dataReader = Conexao.Consultar(comandosql);
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    var idpolitica = (dataReader["IdPolitica"].ToString());
                    listaidpolitica.Add(idpolitica);
                }
            }
            Conexao._conexao.Close();
            foreach (string obj in listaidpolitica)
            {
                comandosql = new SqlCommand(Queries.QUERY_LISTAR_ID_POLITICA);
                comandosql.Parameters.AddWithValue("@Id", obj);
                dataReader = Conexao.Consultar(comandosql);
                dataReader.Read();
                var id = int.Parse(dataReader["Id"].ToString());
                var titulo = dataReader["Titulo"].ToString();
                var data = dataReader["Data"].ToString();
                listadepoliticasassinadas.Politicas.Add(new PoliticaDaListaViewModel(id, titulo, data));
                Conexao._conexao.Close();
            }

            return View("Assinados", listadepoliticasassinadas);
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




        [HttpPost]
        public IActionResult Assinar(int id, bool aceitar)
        {
            if (!aceitar)
            {
                return RedirectToAction("Detalhes", "Home", new { id = id });
            }
            var claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            var comandosql = new SqlCommand(Queries.QUERY_VERIFICAR_ID_FUNCIONARIO);
            comandosql.Parameters.AddWithValue("@IdUsuario", claim.Value);
            var dataRead = Conexao.Consultar(comandosql);
            dataRead.Read();
            var idfuncionario = int.Parse(dataRead["Id"].ToString());
            Conexao._conexao.Close();
            comandosql = new SqlCommand(Queries.QUERY_LISTAR_IDPOLITICA_IDFUNCIONARIO);
            comandosql.Parameters.AddWithValue("IdPolitica", id);
            comandosql.Parameters.AddWithValue("IdFuncionario", idfuncionario);
            dataRead = Conexao.Consultar(comandosql);
            if (dataRead.HasRows)
            {
                TempData["PoliticaAssinada"] = "Política já se encontra assinada!";
                return RedirectToAction("Detalhes", "Home", new { id = id });
            }
            else
            {
                Conexao._conexao.Close();
                comandosql = new SqlCommand(Queries.QUERY_ASSINAR_POLITICA);
                comandosql.Parameters.AddWithValue("@IdPolitica", id);
                comandosql.Parameters.AddWithValue("@IdFuncionario", idfuncionario);
                comandosql.Parameters.AddWithValue("@Data", DateTime.Now);
                Conexao.ExecutarQuery(comandosql);


                TempData["PoliticaAssinadaSucesso"] = "Política assinada com sucesso!";

                return RedirectToAction("Index");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
