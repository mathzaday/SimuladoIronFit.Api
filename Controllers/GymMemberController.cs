using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimuladoIronFit.Api.Data;
using SimuladoIronFit.Api.Models;
using SimuladoIronFit.Api.Models.DTOs;

namespace SimuladoIronFit.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GymMemberController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GymMemberController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymMemberDto>>> GetGymMembers()
        {
            return await _context.GymMembers.Select(g => new GymMemberDto
            {
                Id = g.Id,
                Name = g.Name,
                CPF = g.CPF,
                Email = g.Email,
                Phone = g.Phone,
                PersonalId = g.PersonalId
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GymMemberDto>> GetGymMember(int id)
        {
            var member = await _context.GymMembers.FindAsync(id);

            if (member == null) return NotFound();

            return new GymMemberDto
            {
                Id = member.Id,
                Name = member.Name,
                CPF = member.CPF,
                Email = member.Email,
                Phone = member.Phone,
                PersonalId = member.PersonalId
            };
        }

        [HttpPost]
        public async Task<ActionResult<GymMemberDto>> CreateGymMember([FromBody] GymMemberDto gymMemberDto)
        {
            var member = new GymMember
            {
                Name = gymMemberDto.Name,
                CPF = gymMemberDto.CPF,
                Email = gymMemberDto.Email,
                Phone = gymMemberDto.Phone,
                PersonalId = gymMemberDto.PersonalId
            };

            _context.GymMembers.Add(member);
            await _context.SaveChangesAsync();

            gymMemberDto.Id = member.Id;

            return CreatedAtAction(nameof(GetGymMember), new { id = member.Id }, gymMemberDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGymMember(int id, [FromBody] GymMemberDto gymMemberDto)
        {
            var member = await _context.GymMembers.FindAsync(id);

            if (member == null) return NotFound();

            member.Name = gymMemberDto.Name;
            member.CPF = gymMemberDto.CPF;
            member.Email = gymMemberDto.Email;
            member.Phone = gymMemberDto.Phone;
            member.PersonalId = gymMemberDto.PersonalId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGymMember(int id)
        {
            var member = await _context.GymMembers.FindAsync(id);

            if (member == null) return NotFound();

            _context.GymMembers.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}