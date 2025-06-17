
namespace Password.Generator.Controllers;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using Infrastructure.Models;

[ApiController]
[Route("v1/[controller]")]
public class PasswordGeneratorController(IPasswordGenerator passwordGenerator): ControllerBase
{
    [HttpGet]
    public IActionResult GenerateFromGet([FromQuery]  PasswordOptions? options)
    {
        string password = passwordGenerator.Generate(options);
        return Ok(new {Password = password});
    }

    [HttpPost]
    public IActionResult GenerateFromPost([FromBody] PasswordOptions? options)
    {
        string password = passwordGenerator.Generate(options);
        return Ok(new {Password = password});
    }
} 