using Inventory.Product.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Product.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> opt) : base(opt)
        {

        }
        public DbSet<Item> Products { get; set; }
    }
}
