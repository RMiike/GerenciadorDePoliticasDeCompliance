
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDePoliticasDeCompliance.Core.Dominio
{
    public class Usuario
    {
        public Usuario(string email, string senha)
        {
            Email = email;
            Senha = Criptografia.Criptografia.Encriptar(senha);
        }

        public Usuario(PerfilDeUsuario perfil, string email, string senha)
        {
            Perfil = perfil;
            Email = email;
            Senha = Criptografia.Criptografia.Encriptar(senha);
        }

        public Usuario(int id, PerfilDeUsuario perfil, string email, string senha)
        {
            Id = id;
            Perfil = perfil;
            Email = email;
            Senha =  senha;
        }


        public int Id { get; set; }
        
        public PerfilDeUsuario Perfil { get; set; }
       
      
        [Required]
        [EmailAddress(ErrorMessage = "Email inválido!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
