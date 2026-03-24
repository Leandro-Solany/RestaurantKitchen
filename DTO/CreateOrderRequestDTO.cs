using KitchenRouting.Domain.Enum;

namespace KitchenRouting.DTO
{
    public record CreateOrderRequestDTO(
        IReadOnlyCollection<CreateOrderItemRequest> Items
        );

    public record CreateOrderItemRequest(
        string Name,
        KitchenArea Area
        );

}
