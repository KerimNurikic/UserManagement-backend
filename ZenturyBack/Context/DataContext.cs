using Microsoft.EntityFrameworkCore;
using ZenturyBack.Models;

namespace ZenturyBack.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        public DbSet <User> Users
        {
            get;
            set;
        }

        public DbSet <Login> Logins
        {
            get;
            set;
        }
    }
}
