using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GerenciadorDePoliticasDeCompliance.Core.BancoDeDados
{
    public class Conexao
    {

        public SqlConnection _conexao = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=GerenciadorDePoliticasDeCompliance;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void Conectar()
        {

            if (_conexao.State == System.Data.ConnectionState.Closed)
            {
                _conexao.Open();
            }

        }

        public int ExecutarQuery(SqlCommand comandosql)
        {
            Conectar();
            comandosql.Connection = _conexao;
            int? id = (int?)comandosql.ExecuteScalar();

            Desconectar();
            return id ?? 0;
        }

        private void Desconectar()
        {
            if (_conexao.State == System.Data.ConnectionState.Open)
            {

                _conexao.Close();

            }

                                
        }
      
        public SqlDataReader Consultar(SqlCommand comandosql)
        {
            Conectar();
            comandosql.Connection = _conexao;
            SqlDataReader dataReader = comandosql.ExecuteReader();
            return dataReader;

        }

    }
}