using Api.Manager.Application.Entities;
using Api.Manager.Application.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

public class Utils : IUtils
{
    private readonly PasswordHasher<DataUser> _passwordHasher;
    private readonly byte[] _encryptionKey;
    private readonly byte[] _encryptionIV;

    // Se asume que la clave y el IV se configuran en la app (por ejemplo, en appsettings.json)
    public Utils(PasswordHasher<DataUser> passwordHasher, IConfiguration configuration)
    {
        _passwordHasher = passwordHasher;

        // Se esperan claves de la longitud correcta:
        // Para AES-256, la clave debe tener 32 bytes y el IV 16 bytes.
        string keyString = configuration["Encryption:Key"] ?? throw new ArgumentNullException("Encryption:Key");
        string ivString = configuration["Encryption:IV"] ?? throw new ArgumentNullException("Encryption:IV");

        _encryptionKey = Encoding.UTF8.GetBytes(keyString);
        _encryptionIV = Encoding.UTF8.GetBytes(ivString);
    }

    public string EncryptPassword(DataUser data, string password)
    {
        var contrasenaHasheada = _passwordHasher.HashPassword(data, password);
        return contrasenaHasheada;
    }

    public bool VerifyPassword(DataUser data, string password_hash, string password_in)
    {
        var resultado = _passwordHasher.VerifyHashedPassword(data, password_hash, password_in);
        return resultado == PasswordVerificationResult.Success;
    }

    public string EncryptString(string plainText)
    {
        using Aes aes = Aes.Create();
        aes.Key = _encryptionKey;
        aes.IV = _encryptionIV;

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using MemoryStream ms = new();
        using (CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write))
        using (StreamWriter sw = new(cs, Encoding.UTF8))
        {
            sw.Write(plainText);
        }
        return Convert.ToBase64String(ms.ToArray());
    }

    public string DecryptString(string cipherText)
    {
        using Aes aes = Aes.Create();
        aes.Key = _encryptionKey;
        aes.IV = _encryptionIV;

        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        byte[] buffer = Convert.FromBase64String(cipherText);
        using MemoryStream ms = new(buffer);
        using CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read);
        using StreamReader sr = new(cs, Encoding.UTF8);
        return sr.ReadToEnd();
    }
}
