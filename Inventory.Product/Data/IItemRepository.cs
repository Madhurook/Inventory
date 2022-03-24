
using Inventory.Product.Models;

namespace Inventory.Product.Data
{
    public interface IItemRepository
    { 
        Task<IEnumerable<Item>> GetProducts();
        Task<Item> GetItemById(int Id);

        Task<Item> GetItemByName(string Name);

        Task CreateItem(Item ItemInfo);

        Task UpdateItem(Item ItemInfo);

        Task DeleteItem(int Id);
    }
}
