namespace SimuladoIronFit.Api.Models
{
    public class Scheduling
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int GymMemberId { get; set; }
        public GymMember? GymMember { get; set; }
        public int PersonalId { get; set; }
        public Personal? Personal { get; set; }
    }
}