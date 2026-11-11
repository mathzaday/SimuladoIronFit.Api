using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimuladoIronFit.Api.Data;
using SimuladoIronFit.Api.Models;

namespace SimuladoIronFit.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower() == loginRequest.Username.ToLower());

            if (user == null || user.Password != loginRequest.Password)
            {
                return Unauthorized("Username ou senha inválidos.");
            }
            return Ok(new
            {
                mensagem = "Login realizado com sucesso!",
                id = user.Id,
                nome = user.Name
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User registerRequest)
        {
            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == registerRequest.Username.ToLower()))
            {
                return BadRequest("Este username já está em uso.");
            }

            var novoUsuario = new User
            {
                Name = registerRequest.Name,
                Username = registerRequest.Username,
                Password = registerRequest.Password
            };

            _context.Users.Add(novoUsuario);

            await _context.SaveChangesAsync();
            return CreatedAtAction(
                nameof(Register),
                new { id = novoUsuario.Id },
                new { id = novoUsuario.Id, nome = novoUsuario.Name, username = novoUsuario.Username }
            );
        }
    }
}