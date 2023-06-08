using Microsoft.EntityFrameworkCore;

namespace SwissChatApi.Entities
{
    public class SwissDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public SwissDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("SwissChatDatabase"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Group> Groups { get; set; }
        
    }
}
