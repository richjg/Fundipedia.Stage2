using OrdersDomain.OrderProcessors;
using OrdersModel;

namespace OrdersDomain
{
    public class OrderProcessor(IOrderProcessorLocator orderProcessorLocator) : IOrderProcessor
    {
        private readonly IOrderProcessorLocator orderProcessorLocator = orderProcessorLocator;

        /// <summary>
        /// This is the actual API. how the inners work is kind of inconsequential, 
        /// If we test the API then the inners can be refactored without unit test requiring big changes.
        /// </summary>
        /// <param name="customerVehicleRepairOrder"></param>
        /// <returns></returns>
        public async Task<OrderStatus> ProcessOrderAsync(CustomerVehicleRepairOrder customerVehicleRepairOrder)
        {
            return await (await orderProcessorLocator.FindOrderProcessor(customerVehicleRepairOrder)).ProcessOrderAsync(customerVehicleRepairOrder);
        }
    }
}
