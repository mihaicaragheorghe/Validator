namespace SimpleValidator.Tests;

public class TestUtils
{
    public static User CreateUser(
        string username = "test",
        string email = "test@email.com",
        int age = 18,
        bool isAdmin = false,
        DateTime? createdAt = null)
    {
        return new User(username, email, age, isAdmin, createdAt ?? DateTime.UtcNow);
    }
}