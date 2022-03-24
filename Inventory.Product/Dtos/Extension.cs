using Inventory.Product.Models;

namespace Inventory.Product.Dtos
{
    public static class Extension
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Description);
        }
    }
}
