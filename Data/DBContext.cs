using System;
using Microsoft.EntityFrameworkCore;
using PropertyManager.Models;

namespace PropertyManager.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Command> Commands { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<User> User { get; set; }
    }
}
