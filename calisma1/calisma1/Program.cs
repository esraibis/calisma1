// using Microsoft.EntityFrameworkCore;
using calisma1.Data;
using calisma1.Interfaces;
using calisma1.Repositories;
using calisma1.Services;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// PostgreSQL veritabaný baðlantýsýný yapýlandýrma (ConnectionString'i appsettings.json'dan okuyoruz)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servis ve repository katmanlarýný dependency injection için kaydediyoruz
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPointService, PointService>();

// CORS ayarlarýný ekliyoruz - tüm kaynaklara (origin), yöntemlere ve baþlýklara izin verecek þekilde
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// CORS'u kullanmak için middleware'i ekliyoruz
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

// Eðer kök URL'e eriþilirse, otomatik olarak index.html sayfasýný yükle
app.MapFallbackToFile("/index.html");

app.Run();