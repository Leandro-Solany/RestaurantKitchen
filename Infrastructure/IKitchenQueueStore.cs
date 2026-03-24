using KitchenRouting.Domain;
using KitchenRouting.Domain.Enum;

namespace KitchenRouting.Infrastructure
{
    public interface IKitchenQueueStore
    {
        void Enqueue(KitchenArea area, OrderItem item);

    }
}
