using MicroSample.ProductService.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroSample.ProductService
{
    public interface IAppDbContext
    {
        DbSet<Product> Products { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
