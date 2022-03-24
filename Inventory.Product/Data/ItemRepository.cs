using Inventory.Product.Models;

namespace Inventory.Product.Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly ProductDbContext _dbContext;

        public ItemRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateItem(Item ItemInfo)
        {
            _dbContext.Products.Add(ItemInfo);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task DeleteItem(int Id)
        {
            var existingItem = _dbContext.Products.FirstOrDefault(x => x.Id == Id); 
            _dbContext.Remove(existingItem);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task<Item> GetItemById(int Id)
        {
            var item = _dbContext.Products.FirstOrDefault(x => x.Id == Id);
            return await Task.FromResult(item);
        }

        public async Task<Item> GetItemByName(string Name)
        {
            return await Task.FromResult(_dbContext.Products.FirstOrDefault(x => x.Name == Name));
        }

        public async Task<IEnumerable<Item>> GetProducts()
        {
            return await Task.FromResult(_dbContext.Products);
        } 

        public async Task UpdateItem(Item ItemInfo)
        {
            var existingItem = _dbContext.Products.FirstOrDefault(x => x.Id == ItemInfo.Id);
            existingItem.Name = ItemInfo.Name;
            existingItem.Description = ItemInfo.Description;
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }
    }
}
