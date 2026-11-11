namespace SimuladoIronFit.Api.Models.DTOs
{
    public class GymMemberDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int? PersonalId { get; set; }
    }
}