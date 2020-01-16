namespace GerenciadorDePoliticasDeCompliance.Core.Dominio
{
    public class Funcionario
    {
        public Funcionario(string nome, int cpf, int matricula, string email)
        {
            Nome = nome;
            CPF = cpf;
            Matricula = matricula;
            Email = email;
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public int CPF { get; set; }

        public int Matricula { get; set; }
        
        public string Email { get; set; }

        public Usuario Usuario { get; set; }




    }
}
