using FluentAssertions;
using Inventory.Product.Controllers;
using Inventory.Product.Data;
using Inventory.Product.Dtos;
using Inventory.Product.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Inventory.Product.UnitTest
{
    public class ProductControllerTest
    {
        private readonly Mock<IItemRepository> _itemRepositoryMock = new();

        [Fact]
        public async Task GetProducts_WithNonMatchingItems_ReturnsAllItems()
        {
            var expectedItems = new[] { 
                       new Item() { Id = 9, Name = "Test1", Description = "Test1" },
                       new Item() { Id = 10, Name = "Test2", Description = "Test3" },
                       new Item() { Id = 11, Name = "Test3", Description = "Test3" }
            };

            _itemRepositoryMock.Setup(repo => repo.GetProducts()).ReturnsAsync(expectedItems);

            var controller = new ProductController(_itemRepositoryMock.Object);

            // Act
            var actualItems = await controller.GetProduct();

            // Assert
            actualItems.Result.Should().BeOfType<OkObjectResult>();

            (actualItems.Result as ObjectResult).Value.Should().BeEquivalentTo(expectedItems); 
        }

        [Fact]
        public async Task GetProducts_WithMatchingItems_ReturnsAllItems()
        { 
            var allItems = new[] {
                       new Item() { Id = 9, Name = "Test1", Description = "Test1" },
                       new Item() { Id = 10, Name = "Test2", Description = "Test3" },
                       new Item() { Id = 11, Name = "Test3", Description = "Test3" }
            };

            var expectedItems = new[] {
                       new Item() { Id = 9, Name = "Test1", Description = "Test1" }
            };

            _itemRepositoryMock.Setup(repo => repo.GetProducts()).ReturnsAsync(allItems);

            var controller = new ProductController(_itemRepositoryMock.Object);

            var itemsToMatch = "Test1";

            // Act
            var foundItems = await controller.GetProduct(itemsToMatch); 

            // Assert
            foundItems.Result.Should().BeOfType<OkObjectResult>(); 
            (foundItems.Result as ObjectResult).Value.Should().BeEquivalentTo(expectedItems);
         }

        [Fact]
        public async Task GetProductById_WithExistingItem_ReturnsMatchedItem()
        {
            var item = new Item() { Id = 9, Name = "Test1", Description = "Test1" };

            _itemRepositoryMock.Setup(repo => repo.GetItemById(It.IsAny<int>())).ReturnsAsync(item);

            var controller = new ProductController(_itemRepositoryMock.Object); 

            // Act
            var result = await controller.GetProductById(1);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            (result.Result as ObjectResult).Value.Should().BeEquivalentTo(item);
        }

        [Fact]
        public async Task GetProductById_WithNoExistingItem_ReturnsNotFound()
        { 
            _itemRepositoryMock.Setup(repo => repo.GetItemById(It.IsAny<int>())).ReturnsAsync((Item)null);

            var controller = new ProductController(_itemRepositoryMock.Object);

            // Act
            var result = await controller.GetProductById(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task CreateProduct_WithItemtoCreate_ReturnsCreatedItem()
        {
            var itemToCreate = new CreateItemDto("Create Item", "Create Item"); 
             
            var controller = new ProductController(_itemRepositoryMock.Object);

            // Act
            var result = await controller.CreateProduct(itemToCreate);

            
            // Assert
            var createdItem = (result.Result as CreatedAtActionResult).Value as ItemDto;
            itemToCreate.Should().BeEquivalentTo(
                createdItem,
                options => options.ComparingByMembers<ItemDto>().ExcludingMissingMembers());

            createdItem.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateProduct_WithExistingItem_ReturnsUpdatedItem()
        {
            var existingItem = new Item() { Id = 9, Name = "Test1", Description = "Test1" };

            _itemRepositoryMock.Setup(repo => repo.GetItemById(It.IsAny<int>())).ReturnsAsync(existingItem);

            var controller = new ProductController(_itemRepositoryMock.Object);

            // Act
            var updateItemId = 9;
            var itemsToUpdate = new CreateItemDto("Update Test", "Update Test");
            var result = await controller.UpdateProduct(updateItemId, itemsToUpdate);


            // Assert 
            result.Result.Should().BeOfType<CreatedAtActionResult>();

            var updateItemResult = (result.Result as CreatedAtActionResult).Value as ItemDto;
            updateItemResult.Should().NotBeNull();
            updateItemResult.Name.Should().BeSameAs(itemsToUpdate.Name);
            updateItemResult.Description.Should().BeSameAs(itemsToUpdate.Description);
        }
    }
}