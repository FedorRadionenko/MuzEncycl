using System.Data.Entity;

namespace MuzEncycl
{
    class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection") { }
        public DbSet<MainGenre> MainGenres { get; set; }
        public DbSet<SubGenre> SubGenres { get; set; }
    }
}
