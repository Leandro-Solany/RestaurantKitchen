using KitchenRouting.Domain.Enum;

namespace KitchenRouting.Domain
{
    public record OrderItem(
        string Name,
        KitchenArea Area
        );

}
