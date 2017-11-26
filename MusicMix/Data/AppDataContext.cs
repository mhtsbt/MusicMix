using Microsoft.EntityFrameworkCore;
using MusicMix.Models;

namespace MusicMix.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Vote> Votes { get; set; }

    }
}
