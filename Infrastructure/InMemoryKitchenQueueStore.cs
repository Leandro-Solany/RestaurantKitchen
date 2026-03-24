using KitchenRouting.Domain;
using KitchenRouting.Domain.Enum;
using System.Collections.Concurrent;

namespace KitchenRouting.Infrastructure
{
    public class InMemoryKitchenQueueStore : IKitchenQueueStore
    {
        private readonly ConcurrentDictionary<KitchenArea, ConcurrentQueue<OrderItem>> _queues = new();
        public void Enqueue(KitchenArea area, OrderItem item)
        {
            var queue = _queues.GetOrAdd(area, _ => new ConcurrentQueue<OrderItem>());
            queue.Enqueue(item);
        }
    }
}
