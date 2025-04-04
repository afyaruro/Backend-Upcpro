using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Application.Base.GeneratePassword
{
    public static class PasswordGenerator
    {
        public static string GenerateRandomPassword(int length = 8)
        {
            if (length < 8)
                throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.");

            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";

            RandomNumberGenerator rng = RandomNumberGenerator.Create();

            char GetRandomChar(string chars)
            {
                byte[] buffer = new byte[1];
                rng.GetBytes(buffer);
                return chars[buffer[0] % chars.Length];
            }

            char[] password = new char[length];
            password[0] = GetRandomChar(lowerChars);
            password[1] = GetRandomChar(upperChars);
            password[2] = GetRandomChar(numbers);

            string allChars = lowerChars + upperChars + numbers;
            for (int i = 3; i < length; i++)
            {
                password[i] = GetRandomChar(allChars);
            }

            return new string(password.OrderBy(x => Guid.NewGuid()).ToArray());
        }
    }
}