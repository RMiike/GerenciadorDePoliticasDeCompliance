using GerenciadorDePoliticasDeCompliance.Core.Dominio;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDePoliticasDeCompliance.Web.Models.Politicas
{
    public class FormularioViewModel
    {
        public FormularioViewModel()
        {

        }
        public FormularioViewModel(int id, string titulo, string texto)
        {
            Id = id;
            Titulo = titulo;
            Texto = texto;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Tamanho entre {2} e {1}")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        public string Texto { get; set; }

        public Politica ConverterParaPolitica()
        {
            Politica politica = new Politica(Id, Titulo, Texto);
            return politica;
        }

    }
}

