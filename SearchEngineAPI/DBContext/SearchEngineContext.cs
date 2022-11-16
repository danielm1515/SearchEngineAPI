using Microsoft.EntityFrameworkCore;
using SearchEngineAPI.Models;
using SearchEngineAPI.Models.User;
using System.Reflection;

namespace SearchEngineAPI.DBContext
{
    public class SearchEngineContext : DbContext
    {

        public SearchEngineContext(DbContextOptions<SearchEngineContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(
           new
           {
               UserId = Guid.Parse("570AE72A-F266-4D4B-A207-366CF4D98A16"),
               Email = "danielmamre@gmail.com",
               Password = "dm1234dm",

           });

        }
    }
}
