// An Order is immutable once created
// An Order must have at least one item.
// CreatedAt represents the UTC timestamp qhen the order was created.

namespace KitchenRouting.Domain
{
    public record Order(
        Guid Id,
        IReadOnlyCollection<OrderItem> Items,
        DateTime CreatedAt
        );
}
