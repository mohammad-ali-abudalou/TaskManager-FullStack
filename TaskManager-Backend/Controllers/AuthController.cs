using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TaskManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IConfiguration config) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest login)
    {
        if (login.Username == "admin" && login.Password == "admin")
        {

            // In a real application, you would validate the user credentials against a database.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? "SecretKey_At_2026_Key"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("TaskManager", "TaskUser",
                new[] { new Claim(ClaimTypes.Name, "Admin"), new Claim(ClaimTypes.NameIdentifier, "Admin") },
                expires: DateTime.UtcNow.AddHours(2), signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
        return Unauthorized();
    }

}

public record LoginRequest(string Username, string Password); // This record type is used to represent the login request payload, containing the username and password fields.
