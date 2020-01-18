using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDePoliticasDeCompliance.Web.Models.Politicas
{
    public class DetalheViewModel
    {
        public List<PoliticaDoDetalheViewModel> Politicas { get; set; }

        public DetalheViewModel()
        {
            Politicas = new List<PoliticaDoDetalheViewModel>();
        }

    }

    public class PoliticaDoDetalheViewModel
    {
        public PoliticaDoDetalheViewModel(int id, string titulo, string texto)
        {
            Id = id;
            Titulo = titulo;
            Texto = texto;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }



    }
}
