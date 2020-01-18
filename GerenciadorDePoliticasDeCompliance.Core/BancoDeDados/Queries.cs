using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDePoliticasDeCompliance.Core.BancoDeDados
{
    public class Queries
    {
        public static string QUERY_CADASTRO_FUNCIONARIO = "insert into Funcionario( Nome, CPF, Matricula, Email, IdUsuario) values( @Nome, @CPF, @Matricula, @Email, @IdUsuario); select cast(scope_identity() as int);";
        public static string QUERY_CADASTRO_USUARIO = "insert into Usuario(Email, Senha,IdPerfil) values( @Email, @Senha,  @IdPerfil); select cast(scope_identity() as int);";
        public static string QUERY_CADASTRO_POLITICAS = "insert into Politica(Titulo, Texto, Data) values(@Titulo, @Texto, @Data)";
        public static string QUERY_ATUALIZAR_POLITICA = "update Politica set (Titulo, Texto, Data) values (@Titulo, @Texto, @Data)";
        public static string QUERY_LISTAR_POLITICA = "select * from Politica";
        public static string QUERY_LISTAR_ID_POLITICA = "select * from Politica where id =";
        // TODO escrever strings que sao as queries que irao consultar e gravar os dominios.
        // usar string.format para mesclar os dados dentro das queries. Ex.: string.format("insert into funcionarios values ({0}, {1}", modelo.nome, modelo.id) 
    }
}
