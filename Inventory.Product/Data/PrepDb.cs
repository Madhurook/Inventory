using Inventory.Product.Models;

namespace Inventory.Product.Data
{
    public static class PrepDb
    { 
        public static void PrepDbPopulation(IApplicationBuilder app)
        {
            using (var service = app.ApplicationServices.CreateScope())
            {
                SeedData(service.ServiceProvider.GetService<ProductDbContext>());
            }
        }
        public static void SeedData(ProductDbContext context)
        {
            context.Products.AddRange(
                new List<Item>()
            {
                new Item(){ Id = 1, Name = "Monitor", Description = "Display visual information" },
                new Item(){ Id = 2, Name = "Key Board", Description = "Input the data" },
                new Item(){ Id = 3, Name = "Mouse", Description = "Controlling the cursor" }
            });
            context.SaveChanges();
        }
    }
}
