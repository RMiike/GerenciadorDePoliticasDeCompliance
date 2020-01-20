using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDePoliticasDeCompliance.Core.Dominio
{
    class AsssinaturaPolitica
    {
        public int IdPolitica { get; set; }
        public int IdFuncionario { get; set; }
        public DateTime Date { get; set; }

    }
}
