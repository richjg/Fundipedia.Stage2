using OrdersModel;

namespace OrdersDomain
{
    public interface IRuleBasedOrderProcessor : IOrderProcessor
    {
        Task<bool> CanProcessOrderAsync(CustomerVehicleRepairOrder customerVehicleRepairOrder);
    }
}
