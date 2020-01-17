using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDePoliticasDeCompliance.Core.Dominio
{
    public class Politica
    {

        public int Id { get; set; }

        public string Titulo { get; set; }
        public string Texto { get; set; }

    

      

        public Politica(string titulo, string texto)
        {
            Titulo = titulo;
            Texto = texto;
        }
    }
}
