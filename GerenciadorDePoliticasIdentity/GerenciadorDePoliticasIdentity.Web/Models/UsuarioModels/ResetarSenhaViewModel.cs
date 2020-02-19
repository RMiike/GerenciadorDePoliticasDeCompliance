using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDePoliticasIdentity.Web.Models.UsuarioModels
{
    public class ResetarSenhaViewModel
    {
        public string Token { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "Senhas não conferem!")]
        public string ConfirmarSenha { get; set; }
    }
}
