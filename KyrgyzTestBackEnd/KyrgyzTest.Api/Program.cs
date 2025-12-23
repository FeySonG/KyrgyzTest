using KyrgyzTest.Api.Extensions;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Extensions;
using KyrgyzTest.Application.Services;
using KyrgyzTest.DAL.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region CORS (ТОЛЬКО ТАК ДЛЯ COOKIE + HTTPS)
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .WithOrigins("https://kyrgyztestsystem") // фронт-домен
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
#endregion

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HttpContext
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IHttpAccessorService, HttpAccessorService>();

#region AUTH (Cookies)
builder.Services
    .AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.None;   // 🔴 ОБЯЗАТЕЛЬНО для cross-site
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // 🔴 ТОЛЬКО HTTPS
        options.SlidingExpiration = true;
    });
#endregion

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

#region DB INIT
using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
    initializer.Initialize();
}
#endregion

// ================= PIPELINE =================

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();                // 🔴 ОБЯЗАТЕЛЬНО

app.UseCors("FrontendPolicy");   // 🔴 ДО Auth

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();