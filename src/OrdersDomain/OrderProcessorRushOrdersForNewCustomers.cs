using OrdersModel;

namespace OrdersDomain
{
    public class OrderProcessorRushOrdersForNewCustomers : IRuleBasedOrderProcessor
    {
        public Task<bool> CanProcessOrderAsync(CustomerVehicleRepairOrder customerVehicleRepairOrder)
        {
            var canProcessOrder = customerVehicleRepairOrder.IsRushOrder && customerVehicleRepairOrder.IsNewCustomer;
            return canProcessOrder.AsCompletedTask();
        }
        public Task<OrderStatus> ProcessOrderAsync(CustomerVehicleRepairOrder customerVehicleRepairOrder) => OrderStatus.AuthorisationRequired.AsCompletedTask();
    }
}
