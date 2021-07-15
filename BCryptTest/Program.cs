using System;

namespace BCryptTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please enter a password: ");
            var password1 = Console.ReadLine();
            var hash = BCrypt.Net.BCrypt.HashPassword(password1);
            Console.WriteLine("Hash: {0}", hash);

            Console.Write("Please enter the password again: ");
            var password2 = Console.ReadLine();
            if (BCrypt.Net.BCrypt.Verify(password2, hash))
                Console.WriteLine("The passwords match.");
            else
                Console.WriteLine("The passwords do not match.");
        }
    }
}
