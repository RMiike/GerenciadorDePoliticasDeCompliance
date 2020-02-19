using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDePoliticasIdentity.Web.Models.UsuarioModels
{
    public class DoisFatoresViewModel
    {
        [Required(ErrorMessage = "Token inválido.")]
        public string Token { get; set; }
    }
}
