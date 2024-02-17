using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for input
        Console.WriteLine("Enter a string to hash:");
        string input = Console.ReadLine();

        // Compute the hash
        string hashedString = ComputeSHA256Hash(input);

        // Generate a filename based on current timestamp
        string fileName = $"HashOutput_{DateTime.Now:yyyyMMddHHmmss}.txt";

        // Write hashed string to a text file
        WriteHashedStringToFile(hashedString, fileName);

        Console.WriteLine($"Hashed string has been written to {fileName}");
        Console.ReadLine(); // Keep console open until Enter is pressed
    }

    //  compute to SHA256 hash
    static string ComputeSHA256Hash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2")); // Convert byte to hex string
            }
            return builder.ToString();
        }
    }

    // write hashed string to a text file
    static void WriteHashedStringToFile(string hashedString, string fileName)
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        using (StreamWriter writer = File.CreateText(filePath))
        {
            writer.WriteLine($"Original String: {hashedString}");
            writer.WriteLine($"Hashed String (SHA256): {hashedString}");
        }
    }
}
