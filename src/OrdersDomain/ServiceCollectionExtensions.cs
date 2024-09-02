using Microsoft.Extensions.DependencyInjection;
using OrdersDomain;
using OrdersDomain.OrderProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddOrderDomainServices(this IServiceCollection services)
    {
        services.AddTransient<OrderProcessorLargeRepairOrdersForNewCustomers>();
        services.AddTransient<OrderProcessorLargeRushHireOrders>();
        services.AddTransient<OrderProcessorLargeRepairOrders>();
        services.AddTransient<OrderProcessorRushOrdersForNewCustomers>();
        services.AddTransient<OrderProcessorDefault>();

        services.AddTransient<IOrderProcessorLocator, DefaultOrderProcessorLocator>();
        services.AddTransient<IOrderProcessor, OrderProcessor>();
    }
}

