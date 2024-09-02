using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using OrdersModel;
using System.Net;
using System.Net.Http.Json;

namespace ApiTests
{
    public class OrderApiApplication : WebApplicationFactory<Program>
    {
    }

    public class ApiFunctionalTests
    {
        [TestCase(false, false, false, "Repair", "Confirmed")]
        [TestCase(false, false, false, "Hire", "Confirmed")]
        [TestCase(false, false, true,  "Repair", "Confirmed")]
        [TestCase(false, false, true,  "Hire", "Confirmed")]

        [TestCase(false, true, false, "Repair", "Confirmed")]
        [TestCase(false, true, false, "Hire", "Confirmed")]
        [TestCase(false, true, true,  "Repair", "AuthorisationRequired")]
        [TestCase(false, true, true,  "Hire", "AuthorisationRequired")]

        [TestCase(true, false, false, "Repair", "AuthorisationRequired")]
        [TestCase(true, false, false, "Hire", "Confirmed")]
        [TestCase(true, false, true,  "Repair", "AuthorisationRequired")]
        [TestCase(true, false, true,  "Hire", "Closed")]

        [TestCase(true, true, false, "Repair", "Closed")]
        [TestCase(true, true, false, "Hire", "Confirmed")]
        [TestCase(true, true, true,  "Repair", "Closed")]
        [TestCase(true, true, true,  "Hire", "Closed")]
        public async Task Post_Orders_ReturnsExpected(bool isLargeOrder, bool isNewCustomer, bool isRushOrder, string orderType, string expected)
        {
            var app = new OrderApiApplication();
            var response = await app.CreateClient().PostAsJsonAsync("/api/orders", new {
                IsRushOrder = isRushOrder,
                OrderType = orderType,
                IsNewCustomer = isNewCustomer,
                IsLargeOrder = isLargeOrder
            });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var value = await response.Content.ReadFromJsonAsync<string>();
            Assert.That(value, Is.EqualTo(expected));
        }
    }

}