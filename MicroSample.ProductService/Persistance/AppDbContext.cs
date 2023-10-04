using MicroSample.ProductService.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroSample.ProductService.Persistance
{
    public class AppDbContext:DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
    }
}
