﻿using GerenciadorDePoliticasDeCompliance.Core.BancoDeDados;
using GerenciadorDePoliticasDeCompliance.Core.Dominio;
using GerenciadorDePoliticasDeCompliance.Models.Funcionarios;
using GerenciadorDePoliticasDeCompliance.Web.Models.Funcionarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace GerenciadorDePoliticasDeCompliance.Controllers
{

    [Authorize(Roles = "Administrador")]
    public class FuncionariosController : Controller
    {
        private Conexao Conexao { get; set; }



        public FuncionariosController()
        {
            Conexao = new Conexao();
        }
        public IActionResult Index()
        {
            ListaViewModel lista = new ListaViewModel();
            var comandosql = new SqlCommand(Queries.QUERY_LISTAR_FUNCIONARIOS);
            var dataReader = Conexao.Consultar(comandosql);
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    var nome = dataReader["Nome"].ToString();
                    var cpf = int.Parse(dataReader["CPF"].ToString());
                    var matricula = int.Parse(dataReader["Matricula"].ToString());
                    var email = dataReader["Email"].ToString();

                    lista.Funcionarios.Add(new FuncionarioDaListaViewModel(nome, cpf, matricula, email));


                }
            }
            return View(lista);
        }

        [AllowAnonymous]
        public IActionResult Formulario()
        {
            return View();
        }

        [AllowAnonymous]
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


            return RedirectToAction("Index","Login");
        }



    }
}