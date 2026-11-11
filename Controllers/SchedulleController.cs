using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimuladoIronFit.Api.Data;
using SimuladoIronFit.Api.Models;
using SimuladoIronFit.Api.Models.DTOs;

namespace SimuladoIronFit.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchedulesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SchedulesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleInfoDto>>> GetSchedules()
        {
            return await _context.Schedulings
                .Include(s => s.GymMember)
                .Include(s => s.Personal)
                .Select(s => new ScheduleInfoDto
                {
                    Id = s.Id,
                    Date = s.Date,
                    GymMemberId = s.GymMemberId,
                    MemberName = s.GymMember != null ? s.GymMember.Name : string.Empty,
                    PersonalId = s.PersonalId,
                    PersonalName = s.Personal != null ? s.Personal.Name : string.Empty
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleInfoDto>> GetSchedule(int id)
        {
            var schedule = await _context.Schedulings
                .Include(s => s.GymMember)
                .Include(s => s.Personal)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (schedule == null) return NotFound();

            return new ScheduleInfoDto
            {
                Id = schedule.Id,
                Date = schedule.Date,
                GymMemberId = schedule.GymMemberId,
                MemberName = schedule.GymMember?.Name ?? string.Empty,
                PersonalId = schedule.PersonalId,
                PersonalName = schedule.Personal?.Name ?? string.Empty
            };
        }

        [HttpPost]
        public async Task<ActionResult<ScheduleCreateDto>> CreateSchedule([FromBody] ScheduleCreateDto scheduleDto)
        {
            var schedule = new Scheduling
            {
                Date = scheduleDto.Date,
                GymMemberId = scheduleDto.GymMemberId,
                PersonalId = scheduleDto.PersonalId
            };

            _context.Schedulings.Add(schedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSchedule), new { id = schedule.Id }, scheduleDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] ScheduleCreateDto scheduleDto)
        {
            var schedule = await _context.Schedulings.FindAsync(id);

            if (schedule == null) return NotFound();

            schedule.Date = scheduleDto.Date;
            schedule.GymMemberId = scheduleDto.GymMemberId;
            schedule.PersonalId = scheduleDto.PersonalId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var schedule = await _context.Schedulings.FindAsync(id);

            if (schedule == null) return NotFound();

            _context.Schedulings.Remove(schedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}