using Library;
using Microsoft.EntityFrameworkCore;

namespace GnuOne.Data
{
    public class ApiContext : DbContext
    {
 

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Discussion> Discussion { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LastUpdate> LastUpdates { get; set; }
        public DbSet<MySettings> MySettings { get; set; }


    }
}
