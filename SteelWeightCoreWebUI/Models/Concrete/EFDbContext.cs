using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SteelWeightCoreWebUI.Models.Entities;

namespace SteelWeightCoreWebUI.Models.Concrete {
    public class EFDbContext : DbContext {
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<User>().HasKey(u => new { u.user_name, u.user_pwd });
        }

#if DEBUG
        public DbSet<Steel> test_steel_data { get; set; }
#else
        public DbSet<Steel> steel_data { get; set; }
#endif
        public DbSet<User> users { get; set; }
    }
}
