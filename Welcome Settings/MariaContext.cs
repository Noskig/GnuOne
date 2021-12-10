using Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Welcome_Settings
{
    public class MariaContext : DbContext
    {
        private string _connectionstring;

        public MariaContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Disussion> Discussions {get; set;}
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LastUpdate> LastUpdates { get; set; }
        public DbSet<MySettings> MySettings { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //MariaDb är kompatibel med MySql
            optionsBuilder.UseMySql(_connectionstring, ServerVersion.AutoDetect(_connectionstring));
        }
    }
}
