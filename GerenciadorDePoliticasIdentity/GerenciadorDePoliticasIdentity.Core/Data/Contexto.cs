using GerenciadorDePoliticasIdentity.Core.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDePoliticasIdentity.Core.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {

        }
        public DbSet<Pessoas> Pessoas { get; set; }
    }
}
