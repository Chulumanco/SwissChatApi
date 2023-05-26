namespace SwissChatApi.Entities
{
    using Microsoft.EntityFrameworkCore;
    public class SqliteSwissDBContext : SwissDBContext
    {
        public SqliteSwissDBContext(IConfiguration configuration) : base(configuration) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString("SwissChatDatabase"));
        }
    }
}