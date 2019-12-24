using Microsoft.EntityFrameworkCore;
using TinyURL.Models;

namespace TinyURL.EntityFramework
{
    public class UrlDatabaseContext : DbContext
    {
        public DbSet<Link> Links { get; set; }
        public UrlDatabaseContext(DbContextOptions<UrlDatabaseContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
