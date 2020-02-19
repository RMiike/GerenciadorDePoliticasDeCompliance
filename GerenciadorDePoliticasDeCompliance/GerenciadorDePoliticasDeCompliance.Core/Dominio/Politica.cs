
namespace GerenciadorDePoliticasDeCompliance.Core.Dominio
{
    public class Politica
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public Politica(int id, string titulo, string texto)
        {
            Id = id;
            Titulo = titulo;
            Texto = texto;
        }
    }
}
