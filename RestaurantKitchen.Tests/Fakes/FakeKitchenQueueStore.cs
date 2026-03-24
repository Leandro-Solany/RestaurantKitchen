using KitchenRouting.Domain;
using KitchenRouting.Domain.Enum;
using KitchenRouting.Infrastructure;

namespace KitchenRouting.Tests.Fakes
{
    public class FakeKitchenQueueStore : IKitchenQueueStore
    {
        public List<(KitchenArea Area, OrderItem Item)> EnqueuedItems { get; } = new();

        public void Enqueue(KitchenArea area, OrderItem item)
        {
            EnqueuedItems.Add((area, item));
        }
    }
}
