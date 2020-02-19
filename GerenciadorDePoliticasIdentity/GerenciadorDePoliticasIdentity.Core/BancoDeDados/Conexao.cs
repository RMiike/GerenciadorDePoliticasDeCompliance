using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace GerenciadorDePoliticasIdentity.Core.BancoDeDados
{
   public  class Conexao
    {
        public static DbConnection AbrirConexao()
        {
            var conectar = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=GerenciadorDePoliticasDeComplianceIdentity;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            conectar.Open();
            return conectar;
        }
    }
}
