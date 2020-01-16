using GerenciadorDePoliticasDeCompliance.Core.Dominio;
using System;
using System.ComponentModel.DataAnnotations;


namespace GerenciadorDePoliticasDeCompliance.Web.Models.Politicas
{
    public class FormularioViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Tamanho entre {2} e {1}")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        public string Texto { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        public DateTime Data { get; set; }

       public FormularioViewModel()
        {
            Data = DateTime.Today;
        }


    
    }
}
