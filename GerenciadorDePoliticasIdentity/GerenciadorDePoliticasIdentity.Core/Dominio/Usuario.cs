using System;

namespace GerenciadorDePoliticasIdentity.Core.Dominio
{
    public class Usuario
    {
        public string Id { get; set; }
        public string SenhaHash { get; set; }
        public string Email { get; set; }
        public string EmailNormalizado { get; set; }
        public bool DoisFatoresDeAutenticacao { get; set; }
        public string Telefone { get; set; }
        public bool ConfirmadoEmail { get; set; }
        public bool ConfirmadoTelefone { get; set; }
     
        public bool ContaBloqueada { get; set; }
        public int ContadorDeFalhasDeAcesso { get; set; }
        public Perfil Perfil { get; set; }
        public DateTimeOffset FimDoBloqueio { get; set; }
        public string CarimboDeSeguranca { get; set; }
        public string CarimboAtual { get; set; }
    }
}
