import type { orderTypeEnum } from "./orderTypeEnum";

export interface ICustomerVehicleRepairOrder
{
    isRushOrder:boolean;
    orderType:orderTypeEnum;
    isNewCustomer:boolean;
    isLargeOrder:boolean;
}