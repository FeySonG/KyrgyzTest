using System.Text.Json.Serialization;
using KyrgyzTest.Api.Extensions;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Extensions;
using KyrgyzTest.Application.Services;
using KyrgyzTest.DAL.Extensions;
using KyrgyzTest.OldDb.Extensions;
using KyrgyzTest.OldDb.Seeds;
using KyrgyzTest.OldDbRegion.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",
                "http://127.0.0.1:5173",
                "http://localhost:5174",
                "http://127.0.0.1:5174",
                "https://localhost:5173",
                "https://127.0.0.1:5173",
                "https://kyrgyztestsystem"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IHttpAccessorService, HttpAccessorService>();

builder.Services.AddAuthentication().AddCookie("Cookies", options =>
{
    options.Cookie.HttpOnly = true;
    options.SlidingExpiration = true;
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddOldDbLayer(builder.Configuration);
builder.Services.AddOldDbRegionLayer(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
    initializer.Initialize();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// IIS Express hosts the API under this virtual path. Keep it before routing
// so every controller route remains available as /api/<controller-route>.
app.UsePathBase("/api");

// Avoid redirecting browser CORS preflight requests during local development.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseCors("Frontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


// dotnet publish -c Release -o "C:\inetpub\wwwroot\KyrgyzTestAPI"   