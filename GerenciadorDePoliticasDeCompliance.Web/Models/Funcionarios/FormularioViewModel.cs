using GerenciadorDePoliticasDeCompliance.Core.Dominio;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDePoliticasDeCompliance.Models.Funcionarios
{
    public class FormularioViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Tamanho entre {2} e {1}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatório!")]

        public int  CPF { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        public int  Matricula { get; set; }
        
        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        public string Senha { get; set; }


        public Funcionario ConverterParaFuncionario()
        {
            Funcionario funcionario = new Funcionario(Nome, CPF, Matricula, Email);
            return funcionario;
        }

        public Usuario ConverterParaUsuario()
        {
            Usuario usuario = new Usuario(PerfilDeUsuario.Funcionario, Email, Senha);
            return usuario;
        }
        
    }
}
