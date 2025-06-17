namespace Password.Generator.Infrastructure.Models;

public class PasswordOptions
{
    public int Length { get; set; } = 8;
    public bool IncludeUppercase { get; set; } = true;
    public bool IncludeLowercase { get; set; } = true;
    public bool IncludeDigits { get; set; } = true;
    public bool IncludeSpecialChars { get; set; } = false;
    public bool RandomizeCapitalization { get; set; } = false;
}