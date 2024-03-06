using AspNetCoreRateLimit;
using CM20314.Authentication;
using CM20314.Data;
using CM20314.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container (so they can be acquired via dependency injection).

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = false;
    options.HttpStatusCode = 429;
    options.RealIpHeader = "X-Real-IP";
    options.ClientIdHeader = "X-ClientId";
    options.GeneralRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = "*",
                Period = "1s",
                Limit = 2,
            }
        };
});
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();

builder.Services.AddSingleton<PathfindingService>();
builder.Services.AddSingleton<FileService>();
builder.Services.AddSingleton<MapDataService>();
builder.Services.AddScoped<DbInitialiser>();
builder.Services.AddSingleton<RoutingService>();

var app = builder.Build();

// Initialise Database (on first run) and MapDataService
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    ApplicationDbContext applicationDbContext = services.GetRequiredService<ApplicationDbContext>();
    services.GetRequiredService<DbInitialiser>().Initialise(applicationDbContext, services.GetRequiredService<FileService>());
    services.GetRequiredService<MapDataService>().Initialise(applicationDbContext);
    services.GetRequiredService<RoutingService>().Initialise(
        services.GetRequiredService<PathfindingService>(),
        services.GetRequiredService<MapDataService>(),
        applicationDbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIpRateLimiting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ApiKeyMiddleware>();
app.MapControllers();
app.Run();