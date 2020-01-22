using GerenciadorDePoliticasDeCompliance.Core.Dominio;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDePoliticasDeCompliance.Web.Models.Login
{
    public class LoginViewModel
    {
        public int Id { get; set; }

        [EmailAddress(ErrorMessage = "Email inválido!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        public string Senha { get; set; }
        
        public Usuario ConverterParaUsuarioDeLogin()
        {
            Usuario usuario = new Usuario(Email, Senha);
            return usuario;
        }
        public LoginViewModel()
        {

        }
    }
}
