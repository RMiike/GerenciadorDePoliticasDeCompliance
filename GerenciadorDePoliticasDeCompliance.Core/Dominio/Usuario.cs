
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
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
