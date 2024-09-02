<script setup lang="ts">

import type { ICustomerVehicleRepairOrder } from '@/models/CustomerVehicleRepairOrder';
import { orderTypeEnum } from '@/models/orderTypeEnum';
import OrderService from '@/services/OrderService';
import { ref } from 'vue';

const isRushOrder = ref(false);
const isNewCustomer = ref(false);
const isLargeOrder = ref(false);
const orderType = ref(orderTypeEnum.Repair);
let orderResult = ref('');

async function sendOrder(){

  orderResult.value = '';

  let order:ICustomerVehicleRepairOrder = {
        isRushOrder: isRushOrder.value,
        orderType: orderType.value,
        isNewCustomer: isNewCustomer.value,
        isLargeOrder: isLargeOrder.value
  };
  
  let result = await new OrderService().sendOrder(order);
  orderResult.value = result;

}

</script>

<template>
  <div class="order">
      <div>
        <div>
          <label for="isRushOrder">
            <input id="isRushOrder" name="isRushOrder" type="checkbox" v-model="isRushOrder">
            <span>Rush Order</span>
          </label>
        </div>
        <div>
          <label for="isNewCustomer">
            <input id="isNewCustomer" name="isNewCustomer" type="checkbox" v-model="isNewCustomer">
            <span>New Customer</span>
          </label>
        </div>
        <div>
          <label for="isLargeOrder">
            <input id="isLargeOrder" name="isLargeOrder" type="checkbox" v-model="isLargeOrder">
            <span>Large Order</span>
          </label>
        </div>
        <div class="order-type">
          <label for="orderType">Order Type</label>
          <select id="orderType" name="orderType" v-model="orderType">
            <option :value="orderTypeEnum.Repair">Repair</option>
            <option :value="orderTypeEnum.Hire">Hire</option>
          </select>
        </div>
      </div>
      
      <div class="mt" style="text-align: right;">
        <button @click="sendOrder">Send Order</button>
      </div>

      <div class="mt">
        Result: {{ orderResult }}
      </div>

</div>
</template>

<style scoped>

/*
quick style - normal use things like bootstrap etc.
*/

.order {
  label {
    input {
       margin:0;
       padding:0;
    }

    span {
      margin-left: 10px;
    }
 }

 select {
   margin-left: 10px;
 }

}

.mt{
  margin-top: 1em;
}
</style>
