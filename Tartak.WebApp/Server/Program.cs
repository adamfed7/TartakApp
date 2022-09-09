using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Tartak.WebApp.Library.Data;
using Tartak.WebApp.Server.Data;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        connectionString: configuration.GetConnectionString("TartakAppAuth")
        ));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(config =>
{
    config.Password.RequireUppercase = false;
    config.Password.RequireLowercase = false;
    config.Password.RequiredLength = 4;
    config.Password.RequireDigit = false;
    config.Password.RequireNonAlphanumeric = false;
    config.SignIn.RequireConfirmedEmail = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", JWTVeareroptions =>
{
    JWTVeareroptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5)
    };
});

builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<IShopProductData, ShopProductData>();
builder.Services.AddScoped<IWarehouseProductData, WarehouseProductData>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TartakAPI",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var roles = new List<IdentityRole>();
    roles.Add(new IdentityRole()
    {
        Name = "Admin",
        NormalizedName = "Admin".ToUpperInvariant()
    });
    roles.Add(new IdentityRole()
    {
        Name = "Manager",
        NormalizedName = "Manager".ToUpperInvariant()
    });
    var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    bool creating = context.Database.EnsureCreated();
    if (creating)
    {
        var usermanager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        context.Roles.AddRange(roles);
        context.SaveChanges();
        var admin = new IdentityUser()
        {
            UserName = "Admin",
        };
        await usermanager.CreateAsync(admin);
        admin = await usermanager.FindByNameAsync("Admin");
        await usermanager.AddPasswordAsync(admin, "admin");
        await usermanager.AddToRoleAsync(admin, "Admin");

        var manager = new IdentityUser()
        {
            UserName = "Manager",
        };
        await usermanager.CreateAsync(manager);
        manager = await usermanager.FindByNameAsync("Manager");
        await usermanager.AddPasswordAsync(manager, "manager");
        await usermanager.AddToRoleAsync(manager, "Manager");
        usermanager.Dispose();
    }
    context.Dispose();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
