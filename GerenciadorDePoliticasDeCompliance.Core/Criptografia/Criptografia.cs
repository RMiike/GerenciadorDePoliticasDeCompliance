using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GerenciadorDePoliticasDeCompliance.Core.Criptografia
{
    static class Criptografia
    {

        public static string Encriptar(string valor)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return String.Concat(hash
                  .ComputeHash(Encoding.UTF8.GetBytes(valor))
                  .Select(item => item.ToString("x2")));
            }
        }
    }
}
