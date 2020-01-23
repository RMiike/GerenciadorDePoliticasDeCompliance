using System.Data.SqlClient;

namespace GerenciadorDePoliticasDeCompliance.Core.BancoDeDados
{
    public class Conexao
    {

        public SqlConnection _conexao = new SqlConnection(@"Server=tcp:teste-wiz-renato.database.windows.net,1433;Initial Catalog=Teste-Wiz;Persist Security Info=False;User ID=rmiikea;Password=1A2B3c4d;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

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