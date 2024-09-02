namespace OrdersModel
{
    public record CustomerVehicleRepairOrder(bool IsRushOrder, OrderType OrderType, bool IsNewCustomer, bool IsLargeOrder);
}
