using Microsoft.EntityFrameworkCore;
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

var app = builder.Build();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<ShopContext>();
    bool creating = context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
