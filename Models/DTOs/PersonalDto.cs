namespace SimuladoIronFit.Api.Models.DTOs
{
    public class PersonalDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
    }

    public class CreatePersonalDto
    {
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
    }
}