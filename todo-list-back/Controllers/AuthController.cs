using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using todo_list_back.Context; 
using todo_list_back.Models; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace todo_list_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] Login loginModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = _context.Usuarios
                                   .Include(u => u.Roles) 
                                   .SingleOrDefault(u => u.Nombre == loginModel.Username);

                if (user == null || !VerifyPassword(loginModel.Password, user.Password))
                {
                    return Unauthorized(new { Message = "Usuario o contraseña inválidas." });
                }

                var token = GenerateJwtToken(user);

                return Ok(new
                {
                    Message = "Login exitoso.",
                    Token = token,
                    User = new
                    {
                        user.Id,
                        user.Nombre,
                        user.Id_Roles_FK,
                        RoleName = user.Roles.Nombre
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error interno del servidor", Details = ex.Message });
            }
        }

        private string GenerateJwtToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Nombre),
                new Claim(ClaimTypes.Role, user.Id_Roles_FK.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // Agregar el ID del usuario como claim
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            return enteredPassword == storedPasswordHash; // Implementa aquí la verificación de hash si es necesario
        }
    }
}