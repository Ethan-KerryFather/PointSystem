using Microsoft.EntityFrameworkCore;

namespace PointSystem.Model.Context
{
    public class UsersContext :DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Points> Points { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=D662-ETHANLIM;Database=Test06;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
