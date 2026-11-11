using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimuladoIronFit.Api.Data;
using SimuladoIronFit.Api.Models;
using SimuladoIronFit.Api.Models.DTOs;

namespace SimuladoIronFit.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonalsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonalDto>>> GetPersonals()
        {
            return await _context.Personals.Select(p => new PersonalDto
            {
                Id = p.Id,
                Name = p.Name,
                Specialty = p.Specialty,
                CPF = p.CPF
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalDto>> GetPersonal(int id)
        {
            var personal = await _context.Personals.FindAsync(id);

            if (personal == null) return NotFound();

            return new PersonalDto
            {
                Id = personal.Id,
                Name = personal.Name,
                Specialty = personal.Specialty,
                CPF = personal.CPF
            };
        }

        [HttpPost]
        public async Task<ActionResult<PersonalDto>> CreatePersonal([FromBody] CreatePersonalDto dto)
        {
            var personal = new Personal
            {
                Name = dto.Name,
                Specialty = dto.Specialty,
                CPF = dto.CPF
            };

            _context.Personals.Add(personal);
            await _context.SaveChangesAsync();

            var responseDto = new PersonalDto
            {
                Id = personal.Id,
                Name = personal.Name,
                Specialty = personal.Specialty,
                CPF = personal.CPF
            };

            return CreatedAtAction(nameof(GetPersonal), new { id = personal.Id }, responseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonal(int id)
        {
            var personal = await _context.Personals.FindAsync(id);

            if (personal == null) return NotFound();

            var agendamentos = await _context.Schedulings
                .Where(s => s.PersonalId == id)
                .ToListAsync();
            _context.Schedulings.RemoveRange(agendamentos);

            var alunos = await _context.GymMembers
                .Where(g => g.PersonalId == id)
                .ToListAsync();
            foreach (var aluno in alunos)
            {
                aluno.PersonalId = null;
            }

            _context.Personals.Remove(personal);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}