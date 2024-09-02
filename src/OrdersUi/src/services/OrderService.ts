import type { ICustomerVehicleRepairOrder } from "@/models/CustomerVehicleRepairOrder";
import type { orderStatusEnum } from "@/models/orderStatusEnum";

export default class OrderService
{

    /*
       This needs Di
       The Url needs to come from settings/env so it can be changed for release/deployment etc
    */
    
    async sendOrder(order:ICustomerVehicleRepairOrder) : Promise<orderStatusEnum>  {

        const response = await fetch("https://localhost:7233/api/orders", {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify(order),
        });
    
        if(response.ok){ //in the 200 range
            let json = await response.json();
            return json;
        }
        
        throw new Error('SendOrder failed')
    }
}

