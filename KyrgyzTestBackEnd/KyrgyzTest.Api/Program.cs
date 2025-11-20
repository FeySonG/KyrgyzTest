using KyrgyzTest.Api.Extensions;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Extensions;
using KyrgyzTest.Application.Services;
using KyrgyzTest.DAL.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Добавляем CORS
builder.Services.AddCors(options =>
{   
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // адрес фронта
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HttpContext for usage in the Application layer
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IHttpAccessorService, HttpAccessorService>();


builder.Services.AddAuthentication().AddCookie("Cookies", options =>
{
    options.Cookie.HttpOnly = true; // so the cookie cannot be read from JS
    options.SlidingExpiration = true; // refresh expiration time when active
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// Run database seed
using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
    initializer.Initialize();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowFrontend");

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();