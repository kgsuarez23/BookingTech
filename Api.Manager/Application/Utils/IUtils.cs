using Api.Manager.Application.Entities;

namespace Api.Manager.Application.Utils
{
    public interface IUtils
    {
        string EncryptPassword(DataUser data, string password);
        bool VerifyPassword(DataUser data, string password_hash, string password_in);
        string EncryptString(string plainText);
        string DecryptString(string cipherText);
    }
}
