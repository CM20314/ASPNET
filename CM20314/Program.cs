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

builder.Services.AddSingleton<PathfindingService>();
builder.Services.AddSingleton<FileService>();
builder.Services.AddSingleton<MapDataService>();
builder.Services.AddScoped<DbInitialiser>();
builder.Services.AddScoped<RoutingService>();

var app = builder.Build();

// Initialise Database (on first run) and MapDataService
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    ApplicationDbContext applicationDbContext = services.GetRequiredService<ApplicationDbContext>();
    services.GetRequiredService<DbInitialiser>().Initialise(applicationDbContext);
    services.GetRequiredService<MapDataService>().Initialise(applicationDbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ApiKeyMiddleware>();
app.MapControllers();
app.Run();