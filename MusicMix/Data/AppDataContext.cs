using Microsoft.EntityFrameworkCore;
using MusicMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMix.Data
{
  public class AppDataContext : DbContext
  {
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

    public DbSet<Song> Songs { get; set; }

  }
}
