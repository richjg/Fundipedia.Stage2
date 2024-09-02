using OrdersModel;
using System.Runtime.CompilerServices;

namespace OrdersDomain.OrderProcessors
{
    public class OrderProcessorDefault : IRuleBasedOrderProcessor
    {
        public Task<bool> CanProcessOrderAsync(CustomerVehicleRepairOrder customerVehicleRepairOrder) => true.AsCompletedTask();
        public Task<OrderStatus> ProcessOrderAsync(CustomerVehicleRepairOrder customerVehicleRepairOrder) => OrderStatus.Confirmed.AsCompletedTask();
    }
}
