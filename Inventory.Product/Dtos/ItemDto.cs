using System.ComponentModel.DataAnnotations;

namespace Inventory.Product.Dtos
{
    public record ItemDto(int Id, string Name, string Description);
    public record CreateItemDto([Required] string Name, string Description);
}
