using GerenciadorDePoliticasIdentity.Core.ValidarCpf;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDePoliticasIdentity.Web.Models
{
    public class CadastroViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [CustomValidationCPF(ErrorMessage = "CPF inválido")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [DataType(DataType.Password)]
        public string SenhaHash { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [DataType(DataType.Password)]
        [Compare("SenhaHash", ErrorMessage = "Senhas não conferem!")]
        public string ConfirmarSenha { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email inválido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")]
        [RegularExpression(@"^[2-9]\d{1}-\d{5}\d{4}$", ErrorMessage = "Você deve digitar um telefone no formato ##-#########")]
        [DataType(DataType.PhoneNumber,ErrorMessage = "Telefone inválido.")]
        public string Telefone { get; set; }

        public bool DoisFatoresDeAutenticacao { get; set; }
        
    }
}
