using KitchenRouting.Domain;
using KitchenRouting.Domain.Enum;
using KitchenRouting.Services;
using KitchenRouting.Tests.Fakes;
using Xunit;

public class OrderRoutingServiceTests
{
    [Fact]
    public void Route_Should_Enqueue_Each_Item_To_Its_Corresponding_Kitchen_Area()
    {
        // Arrange
        var store = new FakeKitchenQueueStore();
        var service = new OrderRoutingService(store);

        var order = new Order(
            Guid.NewGuid(),
            new List<OrderItem>
            {
                new OrderItem("Fries", KitchenArea.Fries),
                new OrderItem("Burger", KitchenArea.Grill),
                new OrderItem("Soda", KitchenArea.Drink)
            },
            DateTime.UtcNow
        );

        // Act
        service.Route(order);

        // Assert
        Assert.Equal(3, store.EnqueuedItems.Count);
        Assert.Contains(store.EnqueuedItems, x => x.Area == KitchenArea.Fries);
        Assert.Contains(store.EnqueuedItems, x => x.Area == KitchenArea.Grill);
        Assert.Contains(store.EnqueuedItems, x => x.Area == KitchenArea.Drink);
    }


    [Fact]
    public async Task RouteAsync_Should_Enqueue_Each_Item_To_Its_Corresponding_Kitchen_Area()
    {
        // Arrange
        var store = new FakeKitchenQueueStore();
        var service = new OrderRoutingService(store);
        var order = new Order(
            Guid.NewGuid(),
            new List<OrderItem>
            {
                new OrderItem("Fries", KitchenArea.Fries),
                new OrderItem("Burger", KitchenArea.Grill),
                new OrderItem("Soda", KitchenArea.Drink)
            },
            DateTime.UtcNow
        );
        // Act
        await service.RouteAsync(order);
        // Assert
        Assert.Equal(3, store.EnqueuedItems.Count);
        Assert.Contains(store.EnqueuedItems, x => x.Area == KitchenArea.Fries);
        Assert.Contains(store.EnqueuedItems, x => x.Area == KitchenArea.Grill);
        Assert.Contains(store.EnqueuedItems, x => x.Area == KitchenArea.Drink);
    }
}
