using Microsoft.EntityFrameworkCore;
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

var app = builder.Build();

    if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<WarehouseContext>();
    bool creating = context.Database.EnsureCreated();
}

app.Run();
