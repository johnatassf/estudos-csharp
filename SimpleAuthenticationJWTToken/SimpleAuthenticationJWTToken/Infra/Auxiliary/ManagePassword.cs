using BC = BCrypt.Net.BCrypt;

namespace SimpleAuthenticationJWTToken.Infra.Auxiliary
{
    public class ManagePassword
    {

        public string GetPasswordHash(string passWord)
        {
            return BC.HashPassword(passWord);
        }

        public bool ValidatePassword(string passWord, string hashPassword)
        {
            return BC.Verify(passWord, hashPassword);
        }
    }
}
