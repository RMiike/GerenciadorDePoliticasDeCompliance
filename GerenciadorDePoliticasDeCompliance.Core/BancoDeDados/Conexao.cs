using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace GerenciadorDePoliticasDeCompliance.Core.BancoDeDados
{
    public class Conexao
    {
        private SqlConnection _conexao { get; set; }

        public void Conectar()
        {
            
        }

        public void ExecutarQuery(string query)
        {

        }         
    }
}
