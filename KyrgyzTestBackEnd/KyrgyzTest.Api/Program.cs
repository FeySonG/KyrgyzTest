using System.Text.Json.Serialization;
using KyrgyzTest.Api.Extensions;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Extensions;
using KyrgyzTest.Application.Services;
using KyrgyzTest.DAL.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region CORS (ТОЛЬКО ТАК ДЛЯ COOKIE + HTTPS)
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("https://kyrgyztestsystem")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
#endregion


// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HttpContext
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IHttpAccessorService, HttpAccessorService>();

#region AUTH (Cookies)
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.None;
    });

#endregion

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddOldDbLayer(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

#region DB INIT
using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
    initializer.Initialize();

    var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

    // if (env.IsDevelopment())
    // {
    //     var searchSeeder = scope.ServiceProvider.GetRequiredService<MeiliSearchSeeder>();
    //     await searchSeeder.SeedAsync();
    // }
}
#endregion

// ================= PIPELINE =================

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseCors("Frontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// dotnet publish -c Release -o "C:\inetpub\wwwroot\KyrgyzTestAPI"