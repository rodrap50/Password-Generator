using System.Text;

namespace Password.Generator.Infrastructure.Services;
using Models;

public class PasswordGenerator: IPasswordGenerator
{
    private static readonly Random Random = new();
    private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private const string Digits = "0123456789";
    private const string SpecialChars = "!@#$%^&*()-_=+[]{};:,.<>?";

    public string Generate(PasswordOptions? options)
    {
        options ??= new PasswordOptions();

        var characterPool = BuildCharacterPool(options);
        if (characterPool.Length == 0)
            throw new InvalidOperationException("At least one character type must be included.");

        var passwordChars = new char[options.Length];
        int insertPos = InsertRequiredCharacters(passwordChars, options);
        FillWithRandomCharacters(passwordChars, insertPos, characterPool);
        ShuffleCharacters(passwordChars);

        if (options.RandomizeCapitalization)
            ApplyRandomCapitalization(passwordChars);

        return new string(passwordChars);
    }

    private StringBuilder BuildCharacterPool(PasswordOptions options)
    {
        var characterPool = new StringBuilder();
        if (options.IncludeUppercase) characterPool.Append(Uppercase);
        if (options.IncludeLowercase) characterPool.Append(Lowercase);
        if (options.IncludeDigits) characterPool.Append(Digits);
        if (options.IncludeSpecialChars) characterPool.Append(SpecialChars);
        return characterPool;
    }

    private int InsertRequiredCharacters(char[] passwordChars, PasswordOptions options)
    {
        int insertPos = 0;
        if (options.IncludeUppercase)
            passwordChars[insertPos++] = Uppercase[Random.Next(Uppercase.Length)];
        if (options.IncludeLowercase)
            passwordChars[insertPos++] = Lowercase[Random.Next(Lowercase.Length)];
        if (options.IncludeDigits)
            passwordChars[insertPos++] = Digits[Random.Next(Digits.Length)];
        if (options.IncludeSpecialChars)
            passwordChars[insertPos++] = SpecialChars[Random.Next(SpecialChars.Length)];
        return insertPos;
    }

    private void FillWithRandomCharacters(char[] passwordChars, int startIndex, StringBuilder characterPool)
    {
        for (int i = startIndex; i < passwordChars.Length; i++)
        {
            passwordChars[i] = characterPool[Random.Next(characterPool.Length)];
        }
    }

    private void ShuffleCharacters(char[] chars)
    {
        for (int i = chars.Length - 1; i > 0; i--)
        {
            int j = Random.Next(i + 1);
            (chars[i], chars[j]) = (chars[j], chars[i]);
        }
    }

    private void ApplyRandomCapitalization(char[] passwordChars)
    {
        for (int i = 0; i < passwordChars.Length; i++)
        {
            if (char.IsLetter(passwordChars[i]) && Random.Next(2) == 0)
            {
                passwordChars[i] = char.IsUpper(passwordChars[i])
                    ? char.ToLower(passwordChars[i])
                    : char.ToUpper(passwordChars[i]);
            }
        }
    }
}