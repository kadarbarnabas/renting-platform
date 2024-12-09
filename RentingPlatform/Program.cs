using RentingPlatform;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(GoogleDefaults.AuthenticationScheme, options => 
{
    options.ClientId=builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
    options.ClientSecret=builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
    options.SaveTokens = true;
    options.CallbackPath = "/singnin-google";
});

// Add services to the container.
builder.Services
    .AddSerilog(
        config => config.
        MinimumLevel.Information().
        WriteTo.Console().
        WriteTo.File("log.txt"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RentingPlatformContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
    options.UseLazyLoadingProxies();
}, ServiceLifetime.Scoped);

builder.Services.AddScoped<IUsersService, UsersService>(); //TODO Ide nektek is fell kell sorolni majd a dolgokat értelem szerűen
builder.Services.AddScoped<IAirbnbService, AirbnbService>();
builder.Services.AddScoped<ICarService, CarService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
