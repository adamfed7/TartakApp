using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Tartak.Sklep.Context;
using Tartak.Sklep.Helpers;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShopContext>(options =>
    options.UseSqlServer(
        connectionString: configuration.GetConnectionString("ShopAppData")
        ));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductHelper, ProductHelper>();
builder.Services.AddScoped<IProductConsumer, ProductConsumer>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<IProductConsumer>();
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

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<ShopContext>();
    bool creating = context.Database.EnsureCreated();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
