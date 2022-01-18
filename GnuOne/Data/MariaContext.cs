using Library;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnuOne.Data
{
    public class MariaContext : DbContext
    {
        private string _connectionstring;

        public MariaContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Discussion> Discussions {get; set;}
        public DbSet<Post> Posts { get; set; }
        public DbSet<MyFriend> MyFriends { get; set; }
        public DbSet<MyFriendsFriends> MyFriendsFriends { get; set; }
        public DbSet<LastUpdate> LastUpdates { get; set; }
        public DbSet<MySettings> MySettings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<myProfile> MyProfile { get; set; }
        public DbSet<standardpictures> Standardpictures { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try { 
            optionsBuilder.UseMySql(_connectionstring, ServerVersion.AutoDetect(_connectionstring));
            }
            catch (Exception e){
                Console.WriteLine(e);
            }
        }
    }
}
