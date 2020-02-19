using Dapper;
using GerenciadorDePoliticasIdentity.Core.BancoDeDados;
using GerenciadorDePoliticasIdentity.Core.Dominio;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GerenciadorDePoliticasIdentity.Core.Deposito
{
    public class UsuarioStore :
        IUserStore<Usuario>,
        IUserPasswordStore<Usuario>,
        IUserEmailStore<Usuario>,
        IUserTwoFactorStore<Usuario>,
          IUserPhoneNumberStore<Usuario>,
        IUserAuthenticatorKeyStore<Usuario>,
        IUserSecurityStampStore<Usuario>

    {



        public void Dispose()
        {
        }

        public async Task<Usuario> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (var conectar = Conexao.AbrirConexao())
            {
                return await conectar.QueryFirstOrDefaultAsync<Usuario>(
                    "select * from Usuarios where Id = @Id",
                    new
                    {
                        Id = userId
                    });
            }

        }

        public async Task<Usuario> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            using (var conectar = Conexao.AbrirConexao())
            {
                return await conectar.QueryFirstOrDefaultAsync<Usuario>(
                    "select * from Usuarios where EmailNormalizado = @EmailNormalizado",
                    new
                    {
                        EmailNormalizado = normalizedUserName
                    });
            }
        }

        public Task<string> GetNormalizedUserNameAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailNormalizado);
        }

        public Task<string> GetPasswordHashAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.SenhaHash);
        }

        public Task<string> GetUserIdAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> HasPasswordAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.SenhaHash != null);
        }

        public Task SetNormalizedUserNameAsync(Usuario user, string normalizedName, CancellationToken cancellationToken)
        {
            user.EmailNormalizado = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(Usuario user, string passwordHash, CancellationToken cancellationToken)
        {
            user.SenhaHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Usuario user, string userName, CancellationToken cancellationToken)
        {
            user.Email = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Usuario user, CancellationToken cancellationToken)
        {
            using (var conectar = Conexao.AbrirConexao())
            {
                await conectar.ExecuteAsync(
                    "update Usuarios set Id = @Id, SenhaHash = @SenhaHash, Email = @Email, EmailNormalizado = @EmailNormalizado, ConfirmadoEmail = @ConfirmadoEmail, Telefone = @Telefone, ConfirmadoTelefone = @ConfirmadoTelefone, CarimboDeSeguranca = @CarimboDeSeguranca where Id = @Id",
                    new
                    {
                        Id = user.Id,
                        SenhaHash = user.SenhaHash,
                        Email = user.Email,
                        EmailNormalizado = user.EmailNormalizado,
                        ConfirmadoEmail = user.ConfirmadoEmail,
                        Telefone = user.Telefone,
                        ConfirmadoTelefone = user.ConfirmadoTelefone,
                        CarimboDeSeguranca = user.CarimboDeSeguranca
                    });
            }
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> CreateAsync(Usuario user, CancellationToken cancellationToken)
        {
            using (var conectar = Conexao.AbrirConexao())
            {
                await conectar.ExecuteAsync(
                  "insert into Usuarios (Id, SenhaHash, Email, EmailNormalizado, Perfil, DoisFatoresDeAutenticacao, Telefone, CarimboDeSeguranca) values (@Id, @SenhaHash, @Email, @EmailNormalizado, @Perfil, @DoisFatoresDeAutenticacao, @Telefone, @CarimboDeSeguranca)",
                    new
                    {
                        Id = user.Id,
                        SenhaHash = user.SenhaHash,
                        Email = user.Email,
                        EmailNormalizado = user.EmailNormalizado,
                        Perfil = user.Perfil,
                        DoisFatoresDeAutenticacao = user.DoisFatoresDeAutenticacao,
                        Telefone = user.Telefone,
                        CarimboDeSeguranca = user.CarimboDeSeguranca
                    });
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Usuario user, CancellationToken cancellationToken)
        {
            using (var conectar = Conexao.AbrirConexao())
            {
                await conectar.ExecuteAsync(
                    "delete from Usuarios where Id = @Id",
                    new
                    {
                        Id = user.Id
                    });
            }
            return IdentityResult.Success;
        }

        public Task SetEmailAsync(Usuario user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult<bool>(user.ConfirmadoEmail);
        }

        public Task SetEmailConfirmedAsync(Usuario user, bool confirmed, CancellationToken cancellationToken)
        {
            user.ConfirmadoEmail = confirmed;
            return Task.CompletedTask;
        }

        public async Task<Usuario> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            using (var conectar = Conexao.AbrirConexao())
            {
                return await conectar.QueryFirstOrDefaultAsync<Usuario>(
                    "select * from Usuarios where Email = @Email",
                    new
                    {
                        Email = normalizedEmail
                    });
            }
        }

        public Task<string> GetNormalizedEmailAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailNormalizado);
        }

        public Task SetNormalizedEmailAsync(Usuario user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.EmailNormalizado = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task SetTwoFactorEnabledAsync(Usuario user, bool enabled, CancellationToken cancellationToken)
        {
            user.DoisFatoresDeAutenticacao = enabled;
            return Task.CompletedTask;
        }

        public Task<bool> GetTwoFactorEnabledAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult<bool>(user.DoisFatoresDeAutenticacao);
        }

        public Task SetSecurityStampAsync(Usuario user, string stamp, CancellationToken cancellationToken)
        {
            user.CarimboDeSeguranca = stamp;
            return Task.CompletedTask;

        }

        public Task<string> GetSecurityStampAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.CarimboDeSeguranca);
        }

        public Task SetAuthenticatorKeyAsync(Usuario user, string key, CancellationToken cancellationToken)
        {
            user.Email = key;
            return Task.CompletedTask;
        }

        public Task<string> GetAuthenticatorKeyAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetPhoneNumberAsync(Usuario user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.Telefone = phoneNumber;
            return Task.CompletedTask;
        }

        public Task<string> GetPhoneNumberAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Telefone);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult<bool>(user.ConfirmadoTelefone);
        }

        public Task SetPhoneNumberConfirmedAsync(Usuario user, bool confirmed, CancellationToken cancellationToken)
        {
            user.ConfirmadoTelefone = confirmed;
            return Task.CompletedTask;
        }
    }
}
