using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Tartak.Magazyn.Context;
using Tartak.Magazyn.Helpers;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WarehouseContext>(options =>
    options.UseSqlServer(
        connectionString: configuration.GetConnectionString("WarehouseAppData")
        ));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductHelper, ProductHelper>();
builder.Services.AddScoped<IProductSender, ProductSender>();


builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var ip = Dns.GetHostEntry("rabbitmq").AddressList.FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        cfg.Host(ip.ToString());

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

    if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<WarehouseContext>();
    bool creating = context.Database.EnsureCreated();
}

app.Run();
