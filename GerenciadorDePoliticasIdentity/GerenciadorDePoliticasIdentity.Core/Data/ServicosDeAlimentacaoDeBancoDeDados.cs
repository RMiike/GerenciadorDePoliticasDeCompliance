using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDePoliticasIdentity.Core.Data
{
    class ServicosDeAlimentacaoDeBancoDeDados
    {
        private readonly Contexto _contexto;
        public ServicosDeAlimentacaoDeBancoDeDados(Contexto contexto)
        {
            _contexto = contexto;
        }
    }
}
