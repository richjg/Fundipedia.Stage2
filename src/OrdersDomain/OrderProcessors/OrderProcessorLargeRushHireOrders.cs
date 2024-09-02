using OrdersModel;

namespace OrdersDomain.OrderProcessors
{
    public class OrderProcessorLargeRushHireOrders : IRuleBasedOrderProcessor
    {
        public Task<bool> CanProcessOrderAsync(CustomerVehicleRepairOrder customerVehicleRepairOrder)
        {
            var canProcessOrder = customerVehicleRepairOrder.IsLargeOrder && customerVehicleRepairOrder.IsRushOrder && customerVehicleRepairOrder.OrderType == OrderType.Hire;
            return canProcessOrder.AsCompletedTask();
        }
        public Task<OrderStatus> ProcessOrderAsync(CustomerVehicleRepairOrder customerVehicleRepairOrder) => OrderStatus.Closed.AsCompletedTask();
    }
}
