using Microsoft.Extensions.DependencyInjection;
using OrdersDomain;
using OrdersDomain.OrderProcessors;
using OrdersModel;
using System.Net.Http.Headers;

namespace FundipediaServiceTests
{
    public class OrderProcessorTests
    {
        public OrderProcessor NewOrderProcessor()
        {
            var defaultOrderProcessorLocator = new DefaultOrderProcessorLocator(new (), new(), new(), new (), new());
            return new OrderProcessor(defaultOrderProcessorLocator);
        }

        /*
          Spec
           - Large repair orders for new customers should be closed
           - Large rush hire orders should always be closed
           - Large repair orders always require authorisation
           - All rush orders for new customers always require authorisation
           - All other orders should be confirmed
         */

        [TestCase(false, false, false, OrderType.Repair, OrderStatus.Confirmed)]
        [TestCase(false, false, false, OrderType.Hire,   OrderStatus.Confirmed)]
        [TestCase(false, false, true,  OrderType.Repair, OrderStatus.Confirmed)]
        [TestCase(false, false, true,  OrderType.Hire,   OrderStatus.Confirmed)]

        [TestCase(false, true,  false, OrderType.Repair, OrderStatus.Confirmed)]
        [TestCase(false, true,  false, OrderType.Hire,   OrderStatus.Confirmed)]
        [TestCase(false, true,  true,  OrderType.Repair, OrderStatus.AuthorisationRequired)]
        [TestCase(false, true,  true,  OrderType.Hire,   OrderStatus.AuthorisationRequired)]

        [TestCase(true,  false, false, OrderType.Repair, OrderStatus.AuthorisationRequired)]
        [TestCase(true,  false, false, OrderType.Hire,   OrderStatus.Confirmed)]
        [TestCase(true,  false, true,  OrderType.Repair, OrderStatus.AuthorisationRequired)]
        [TestCase(true,  false, true,  OrderType.Hire,   OrderStatus.Closed)]

        [TestCase(true,  true,  false, OrderType.Repair, OrderStatus.Closed)]
        [TestCase(true,  true,  false, OrderType.Hire,   OrderStatus.Confirmed)]
        [TestCase(true,  true,  true,  OrderType.Repair, OrderStatus.Closed)]
        [TestCase(true,  true,  true,  OrderType.Hire,   OrderStatus.Closed)]

        public async Task ProcessOrderAsync_ReturnsExpectedOrderStatus_When(bool isLargeOrder, bool isNewCustomer, bool isRushOrder, OrderType orderType, OrderStatus expected)
        {
            //Arrange
            var orderProcessor = NewOrderProcessor();
            var model = new CustomerVehicleRepairOrder(isRushOrder, orderType, isNewCustomer, isLargeOrder);
           
            //Act
            var result = await orderProcessor.ProcessOrderAsync(model);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}