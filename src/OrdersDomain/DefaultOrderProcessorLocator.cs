using OrdersDomain.OrderProcessors;
using OrdersModel;

namespace OrdersDomain
{
    public class DefaultOrderProcessorLocator(OrderProcessorLargeRepairOrdersForNewCustomers largeRepairOrdersForNewCustomers,
                               OrderProcessorLargeRushHireOrders largeRushHireOrders,
                               OrderProcessorLargeRepairOrders largeRepairOrders,
                               OrderProcessorRushOrdersForNewCustomers rushOrdersForNewCustomers,
                               OrderProcessorDefault defaultOrderProcessor) : IOrderProcessorLocator
    {
        private readonly OrderProcessorLargeRepairOrdersForNewCustomers largeRepairOrdersForNewCustomers = largeRepairOrdersForNewCustomers;
        private readonly OrderProcessorLargeRushHireOrders largeRushHireOrders = largeRushHireOrders;
        private readonly OrderProcessorLargeRepairOrders largeRepairOrders = largeRepairOrders;
        private readonly OrderProcessorRushOrdersForNewCustomers rushOrdersForNewCustomers = rushOrdersForNewCustomers;
        private readonly OrderProcessorDefault defaultOrderProcessor = defaultOrderProcessor;

        public async Task<IOrderProcessor> FindOrderProcessor(CustomerVehicleRepairOrder customerVehicleRepairOrder)
        {
            //
            // The read.me states about being more ~ dynamic, just wanted to see what a switch expression would look like ;)
            // - The future issue with the switch expression, would be (IF) the condition to be able to process became more complex i.e say it required an out of proc call. Then having it in its own class allows it to DI its own dependencies.
            //
            // Any way left it commented out
            //

            //var orderProcessor = (IOrderProcessor)(customerVehicleRepairOrder switch
            //{
            //    { IsLargeOrder: true, IsNewCustomer: true, OrderType: OrderType.Repair } => largeRepairOrdersForNewCustomers,
            //    { IsLargeOrder: true, IsRushOrder: true, OrderType: OrderType.Hire } => largeRushHireOrders,
            //    { IsLargeOrder: true, OrderType: OrderType.Repair } => largeRepairOrders,
            //    { IsRushOrder: true, IsNewCustomer: true } => rushOrdersForNewCustomers,
            //    _ => defaultOrderProcessor
            //});

            //return orderProcessor.AsCompletedTask();

            foreach (var orderProcessor in GetOrderProcessorsInPriority())
            {
                if (await orderProcessor.CanProcessOrderAsync(customerVehicleRepairOrder))
                {
                    return orderProcessor;
                }
            }

            return defaultOrderProcessor;
        }

        private IEnumerable<IRuleBasedOrderProcessor> GetOrderProcessorsInPriority()
        {
            /*
                Applied in priority order from top to bottom:

                - Large repair orders for new customers should be closed
                - Large rush hire orders should always be closed
                - Large repair orders always require authorisation
                - All rush orders for new customers always require authorisation
                - All other orders should be confirmed
             */

            yield return largeRepairOrdersForNewCustomers;
            yield return largeRushHireOrders;
            yield return largeRepairOrders;
            yield return rushOrdersForNewCustomers;
            yield return defaultOrderProcessor;
        }
    }
}
