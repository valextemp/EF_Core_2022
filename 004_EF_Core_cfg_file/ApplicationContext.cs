using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace _004_EF_Core_cfg_file
{
    public class ApplicationContext:DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
                : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
