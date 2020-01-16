﻿using System.ComponentModel.DataAnnotations;

namespace GerenciadorDePoliticasDeCompliance.Models.Funcionarios
{
    public class FormularioViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatório!")]

        public int  CPF { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        public int  Matricula { get; set; }
        
        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatório!")]
        public string Senha { get; set; }
    }
}
