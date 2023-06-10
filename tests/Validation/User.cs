namespace SimpleValidator.Tests;

public record User(
    string Username,
    string Email,
    int Age,
    bool IsAdmin,
    DateTime CreatedAt
);