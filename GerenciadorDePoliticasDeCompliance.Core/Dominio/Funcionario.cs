namespace GerenciadorDePoliticasDeCompliance.Core.Dominio
{
    class Funcionario
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }

        public int CPF { get; set; }

        public int Matricula { get; set; }
        
        public string Email { get; set; }

        public string Senha { get; set; }
    }
}
