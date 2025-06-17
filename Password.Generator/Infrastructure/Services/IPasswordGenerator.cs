using Password.Generator.Infrastructure.Models;

namespace Password.Generator.Infrastructure.Services;

public interface IPasswordGenerator
{
    public string Generate(PasswordOptions? options = null);
}