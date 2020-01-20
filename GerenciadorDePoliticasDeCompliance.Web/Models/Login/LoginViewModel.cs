using GerenciadorDePoliticasDeCompliance.Core.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDePoliticasDeCompliance.Web.Models.Login
{
    public class LoginViewModel
    {

        public int Id { get; set; }

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
