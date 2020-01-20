using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDePoliticasDeCompliance.Web.Models.Home
{
    public class HomeViewModel
    {
        public HomeViewModel(int idPolitica, int idFuncionario, DateTime date)
        {
            IdPolitica = idPolitica;
            IdFuncionario = idFuncionario;
            Date = date;
        }

        public int IdPolitica { get; set; }
        public int IdFuncionario { get; set; }
        public DateTime Date { get; set; }


    }
}
