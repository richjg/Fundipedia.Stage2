using OrdersModel;

namespace OrdersDomain.OrderProcessors
{
    public class OrderProcessorLargeRepairOrders : IRuleBasedOrderProcessor
    {
        public Task<bool> CanProcessOrderAsync(CustomerVehicleRepairOrder customerVehicleRepairOrder)
        {
            var canProcessOrder = customerVehicleRepairOrder.IsLargeOrder && customerVehicleRepairOrder.OrderType == OrderType.Repair;
            return canProcessOrder.AsCompletedTask();
        }
        public Task<OrderStatus> ProcessOrderAsync(CustomerVehicleRepairOrder customerVehicleRepairOrder) => OrderStatus.AuthorisationRequired.AsCompletedTask();
    }
}
