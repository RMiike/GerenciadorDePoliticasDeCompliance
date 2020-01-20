namespace GerenciadorDePoliticasDeCompliance.Core.BancoDeDados
{
    public class Queries
    {
        public static string QUERY_CADASTRO_FUNCIONARIO = "insert into Funcionario( Nome, CPF, Matricula, Email, IdUsuario) values( @Nome, @CPF, @Matricula, @Email, @IdUsuario); select cast(scope_identity() as int);";
        public static string QUERY_CADASTRO_USUARIO = "insert into Usuario(Email, Senha,IdPerfil) values( @Email, @Senha,  @IdPerfil); select cast(scope_identity() as int);";
        public static string QUERY_CADASTRO_POLITICAS = "insert into Politica(Titulo, Texto, Data) values(@Titulo, @Texto, @Data)";
        public static string QUERY_ATUALIZAR_POLITICA = "update Politica set Titulo = @Titulo, Texto = @Texto, Data = @Data where id = @Id";
        public static string QUERY_LISTAR_POLITICA = "select * from Politica";
        public static string QUERY_LISTAR_FUNCIONARIOS = "select * from Funcionario";
        public static string QUERY_LISTAR_ID_POLITICA = "select * from Politica where Id = @Id";
        public static string QUERY_DELETAR_POLITICA = "delete from Politica where Id = @Id";
        public static string QUERY_AUTENTICAR_USUARIO = "select top 1 * from Usuario where Email = @Email";
        public static string QUERY_ASSINAR_POLITICA = "update AssinaturaPolitica set IdFuncionario = @IdFuncionario, IdPolitica = @IdPolitica";

    }
}
