﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDePoliticasDeCompliance.Web.Models.Politicas
{
    public class ListaViewModel
    {
        public List<PoliticaDaListaViewModel> Politicas { get; set; }

        public ListaViewModel()
        {
            Politicas = new List<PoliticaDaListaViewModel>();
        }

    }

    public class PoliticaDaListaViewModel
    {
        public PoliticaDaListaViewModel(int id, string titulo, string data)
        {
            Id = id;
            Titulo = titulo;
            Data = data;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Data { get; set; }



    }
}
