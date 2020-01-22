using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDePoliticasDeCompliance.Web.Models.Funcionarios
{
    public class ListaViewModel
    {
        public List<FuncionarioDaListaViewModel> Funcionarios { get; set; }
        public ListaViewModel()
        {
            Funcionarios = new List<FuncionarioDaListaViewModel>();
        }
    }
    public class FuncionarioDaListaViewModel
    {
        public FuncionarioDaListaViewModel(int id, string nome, int cPF, int matricula, string email)
        {
            Nome = nome;
            CPF = cPF;
            Matricula = matricula;
            Email = email;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CPF { get; set; }
        public int Matricula { get; set; }


        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        [EmailAddress(ErrorMessage = "Email inválido!")]
        public string Email { get; set; }
    }
}


