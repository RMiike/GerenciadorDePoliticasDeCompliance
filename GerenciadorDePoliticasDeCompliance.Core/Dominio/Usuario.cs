using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDePoliticasDeCompliance.Core.Dominio
{
    public class Usuario
    {
        public Usuario(PerfilDeUsuario perfil, string email, string senha)
        {
            Perfil = perfil;
            Email = email;
            Senha = senha;
        }

        public int Id { get; set; }
        public PerfilDeUsuario Perfil { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

    }
}
