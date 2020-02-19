using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDePoliticasIdentity.Web.Models.UsuarioModels
{
    public class EsqueciSenhaViewModel
    {
        [DataType(DataType.EmailAddress, ErrorMessage = "Email inválido.")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Email { get; set; }
    }
}
