using System.Data.Entity;

namespace MuzEncycl
{
    class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection") { }
        public DbSet<MainGenre> Genres { get; set; }
        
    }
}
