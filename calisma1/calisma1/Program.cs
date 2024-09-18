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

// PostgreSQL veritaban� ba�lant�s�n� yap�land�rma (ConnectionString'i appsettings.json'dan okuyoruz)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servis ve repository katmanlar�n� dependency injection i�in kaydediyoruz
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPointService, PointService>();

// CORS ayarlar�n� ekliyoruz - t�m kaynaklara (origin), y�ntemlere ve ba�l�klara izin verecek �ekilde
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

// CORS'u kullanmak i�in middleware'i ekliyoruz
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

// E�er k�k URL'e eri�ilirse, otomatik olarak index.html sayfas�n� y�kle
app.MapFallbackToFile("/index.html");

app.Run();