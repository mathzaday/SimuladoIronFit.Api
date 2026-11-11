namespace SimuladoIronFit.Api.Models.DTOs
{
    public class ScheduleCreateDto
    {
        public DateTime Date { get; set; }
        public int GymMemberId { get; set; }
        public int PersonalId { get; set; }
    }

    public class ScheduleInfoDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int GymMemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public int PersonalId { get; set; }
        public string PersonalName { get; set; } = string.Empty;
    }
}