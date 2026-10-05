namespace microfundamento_8_desenvolvimento_web_back_end
{
    using microfundamento_8_desenvolvimento_web_back_end.Models;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}