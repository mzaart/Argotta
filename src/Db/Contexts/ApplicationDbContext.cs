using Microsoft.EntityFrameworkCore;
using Multilang.Models.Db;
using Multilang.Services.ConfigurationServices;
using System.Collections.Generic;
using System.Linq;

namespace Multilang.Db.Contexts
{
    public class ApplicationDbContext: DbContext
    {
        private IConfigService config;

        public ApplicationDbContext(IConfigService config)
        {
            this.config = config;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite(config.GetDbConnectionString());
            optionsBuilder.UseSqlServer(config.GetDbConnectionString());
        }
    }
}