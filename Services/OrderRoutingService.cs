using KitchenRouting.Domain;
using KitchenRouting.Infrastructure;

namespace KitchenRouting.Services
{
    public class OrderRoutingService : IOrderRoutingService
    {
        private readonly IKitchenQueueStore _store;
        public OrderRoutingService(IKitchenQueueStore store)
        {
            _store = store;
        }
        public void Route(Order order)
        {
            foreach (var item in order.Items)
            {
                _store.Enqueue(item.Area, item);
            }
        }

        public Task RouteAsync(Order order, CancellationToken cancellationToken = default)
        {
            foreach (var item in order.Items)
            {
                cancellationToken.ThrowIfCancellationRequested();
                _store.Enqueue(item.Area, item);
            }
            return Task.CompletedTask;
        }
    }
}
