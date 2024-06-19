using WebApplication4.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions)
            : base(contextOptions)
        {

        }



        //public DbSet<Employees> Employees { get; set; }
        public DbSet<SProduct> SProducts { get; set; }

        public DbSet<SAuthViewModel> SUser { get; set; }

        public DbSet<AdminViewModel> Admin { get; set; }
    }
}
