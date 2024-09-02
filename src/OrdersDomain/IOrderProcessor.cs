using OrdersModel;

namespace OrdersDomain
{
    public interface IOrderProcessor
    {
        Task<OrderStatus> ProcessOrderAsync(CustomerVehicleRepairOrder customerVehicleRepairOrder);
    }
}
