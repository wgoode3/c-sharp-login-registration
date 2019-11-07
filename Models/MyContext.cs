using Microsoft.EntityFrameworkCore;
using Unicorns.Models;

namespace Unicorns.Models {
    public class MyContext : DbContext {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {get;set;}
    }
}