using System;
using System.Security.Cryptography;

namespace Seguridad
{
    public static class Encriptador
    {
        private const int SaltSize = 16; // 128 bits
        private const int HashSize = 20; // 160 bits
        private const int Iterations = 100000;

        public static string Hash(string contraseña)
        {
            // 1. Generar Salt aleatorio
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            // 2. Generar el Hash
            using (var pbkdf2 = new Rfc2898DeriveBytes(contraseña, salt, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                // 3. Combinar Salt + Hash para guardar en la BD
                byte[] hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerificarContraseña(string contraseñaIngresada, string hashAlmacenado)
        {
            // 1. Obtener bytes del string Base64
            byte[] hashBytes = Convert.FromBase64String(hashAlmacenado);
            // 2. Extraer el Salt
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);
            // 3. Hashear la contraseña ingresado usando el MISMO salt e iteraciones
            using (var pbkdf2 = new Rfc2898DeriveBytes(contraseñaIngresada, salt, Iterations))
            {
                byte[] hashCalculado = pbkdf2.GetBytes(HashSize);
                // 4. Comparar byte por byte
                for (int i = 0; i < HashSize; i++)
                {
                    if (hashBytes[i + SaltSize] != hashCalculado[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
