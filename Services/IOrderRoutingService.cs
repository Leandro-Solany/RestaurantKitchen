using KitchenRouting.Domain;

namespace KitchenRouting.Services
{
    public interface IOrderRoutingService
    {
        void Route(Order order);
        Task RouteAsync(Order order, CancellationToken cancellationToken = default);
    }
}
