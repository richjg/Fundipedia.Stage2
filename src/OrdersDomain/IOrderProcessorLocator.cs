using OrdersModel;

namespace OrdersDomain
{
    public interface IOrderProcessorLocator
    {
        Task<IOrderProcessor> FindOrderProcessor(CustomerVehicleRepairOrder customerVehicleRepairOrder);
    }
}
