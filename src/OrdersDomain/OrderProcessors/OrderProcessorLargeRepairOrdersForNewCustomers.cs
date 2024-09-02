using OrdersModel;

namespace OrdersDomain.OrderProcessors
{
    public class OrderProcessorLargeRepairOrdersForNewCustomers : IRuleBasedOrderProcessor
    {
        public Task<bool> CanProcessOrderAsync(CustomerVehicleRepairOrder customerVehicleRepairOrder)
        {
            var canProcessOrder = customerVehicleRepairOrder.IsLargeOrder && customerVehicleRepairOrder.OrderType == OrderType.Repair && customerVehicleRepairOrder.IsNewCustomer;
            return canProcessOrder.AsCompletedTask();
        }

        public Task<OrderStatus> ProcessOrderAsync(CustomerVehicleRepairOrder customerVehicleRepairOrder)
        {
            return OrderStatus.Closed.AsCompletedTask();
        }
    }
}
