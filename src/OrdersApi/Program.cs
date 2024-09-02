using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OrdersDomain;
using OrdersModel;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOrderDomainServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(p =>
    {
        p.SetIsOriginAllowedToAllowWildcardSubdomains()
         .SetIsOriginAllowed(o => true)
         .AllowAnyMethod()
         .AllowAnyHeader()
         .SetPreflightMaxAge(TimeSpan.FromDays(1));
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();


app.MapPost("/api/orders", ([FromServices]IOrderProcessor orderProcessor, CustomerVehicleRepairOrder order) =>
{
   return orderProcessor.ProcessOrderAsync(order);
})
.WithName("AddOrder")
.WithOpenApi();

app.Run();

//So unit test can pick it up
public partial class Program { }