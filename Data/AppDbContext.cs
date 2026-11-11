using Microsoft.EntityFrameworkCore;
using SimuladoIronFit.Api.Models;

namespace SimuladoIronFit.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<GymMember> GymMembers { get; set; }
        public DbSet<Personal> Personals { get; set; }
        public DbSet<Scheduling> Schedulings { get; set; }
        public DbSet<User> Users { get; set; }
    }
}