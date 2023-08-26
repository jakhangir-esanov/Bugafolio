using BCrypt.Net;

namespace FinancialPortfolioManagement.Service.Securities;

public static class PasswordHasher
{
    public static (string Password, string Salt) Hash(string password)
    {
        string salt = Guid.NewGuid().ToString();
        string hash = BCrypt.Net.BCrypt.HashPassword(password + salt);
        return (Password: hash, Salt: salt);
    }

    public static bool Verify(string password, string passwordDatabase, string salt)
    {
        return BCrypt.Net.BCrypt.Verify(password + salt, passwordDatabase);
    }
}
