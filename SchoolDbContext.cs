using Microsoft.EntityFrameworkCore;

namespace FREDRIK_JONSSON_SUT24_LABB3
{
    public class SchoolDbContext : DbContext
    {
        //public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

    }
}
