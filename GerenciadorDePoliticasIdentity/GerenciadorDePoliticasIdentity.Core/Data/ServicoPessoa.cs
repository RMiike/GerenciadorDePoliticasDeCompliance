using GerenciadorDePoliticasIdentity.Core.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDePoliticasIdentity.Core.Data
{
    public class ServicoPessoa
    {
        private readonly Contexto _contexto;

        public ServicoPessoa(Contexto contexto)
        {
            _contexto = contexto;
        }
        public void Inserir(Pessoas pessoa)
        {
            _contexto.Add(pessoa);
            _contexto.SaveChanges();
        }
    }
}
