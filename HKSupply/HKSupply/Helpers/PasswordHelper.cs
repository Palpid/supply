﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Helpers
{
    /// <summary>
    /// Clase de ayuda para la gestíón de password, conseguir el hash y validar un password
    /// </summary>
    public class PasswordHelper
    {
        private static readonly Random random = new Random();
        public static string GetHash(string plainText, byte[] saltBytes = null)
        {
            if (saltBytes == null)
            {
                int minSaltSize = 4;
                int maxSaltSize = 8;

                // determina el tamaño de la sal
                int saltSize = random.Next(minSaltSize, maxSaltSize);
                saltBytes = new byte[saltSize];

                // Utiliza un generador seguro de números aleatorios
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                // Obtiene la sal con el generador
                rng.GetNonZeroBytes(saltBytes);
            }

            // Convierte la contraseña a array de bytes
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Array para guardar la contraseña y la sal
            byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltBytes.Length];

            // Almacena la contraseña
            for (int i = 0; i < plainTextBytes.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainTextBytes[i];
            }

            // Añade la sal
            for (int i = 0; i < saltBytes.Length; i++)
            {
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];
            }

            // Especifica el algoritmo SHA512
            HashAlgorithm hash = new SHA512Managed();

            // Calcula el hash
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            // Array para almacenar el hash y la sal
            byte[] hashWithSaltBytes = new byte[hashBytes.Length + saltBytes.Length];

            // Copia el hash en el array
            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashWithSaltBytes[i] = hashBytes[i];
            }

            // Añade la sal
            for (int i = 0; i < saltBytes.Length; i++)
            {
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];
            }

            // Convierte el resultado a cadena y lo devuelve
            return Convert.ToBase64String(hashWithSaltBytes);
        }

        public static bool ValidatePass(string pass, string hash)
        {
            // Convierte al hash a array de bytes
            byte[] hashWithSaltBytes = Convert.FromBase64String(hash);

            // Tamaño del hash en bits
            int hashSizeInBits, hashSizeInBytes;
            hashSizeInBits = 512;

            // Convierte el tamaño a bytes
            hashSizeInBytes = hashSizeInBits / 8;

            // Verifica que el valor es lo suficientemente largo
            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            // Array para almacenar la sal
            byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

            // Copia la sal al array
            for (int i = 0; i < saltBytes.Length; i++)
            {
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];
            }

            // Calcula de nuevo el hash
            string expectedHashString = GetHash(pass, saltBytes);

            // Si ambos hash coinciden la contraseña es correcta
            return (hash == expectedHashString);
        }
    }
}
