using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SuplementosFGFit_Back;
using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Repositorios.IRepositorio;
using SuplementosFGFit_Back.Repositorios.Repositorio;
using SuplementosFGFit_Back.Services;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SuplementosFgfitContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<IFormaEnvioRepositorio, FormaEnvioRepositorio>();
builder.Services.AddScoped<IFormasPagoRepositorio, FormasPagoRepositorio>();
builder.Services.AddScoped<IUnidadesMedidaRepositorio, UnidadesMedidaRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IUsuarioRolRepositorio, UsuarioRolRepositorio>();
builder.Services.AddScoped<IProveedorRepositorio, ProveedorRepositorio>();
builder.Services.AddScoped<IProductoRepositorio, ProductoRepositorio>();
builder.Services.AddScoped<IProductoProveedorRepositorio, ProductoProveedorRepositorio>();
builder.Services.AddScoped<IProveedorFormaPagoRepositorio, ProveedorFormaPagoRepositorio>();
builder.Services.AddScoped<IProveedorFormaEnvioRepositorio, ProveedorFormaEnvioRepositorio>();
builder.Services.AddScoped<IOrdenCompraRepositorio, OrdenCompraRepositorio>();
builder.Services.AddScoped<IDetalleOrdenCompraRepositorio, DetalleOrdenCompraRepositorio>();
builder.Services.AddScoped<IEstadoOrdenCompraRepositorio, EstadoOrdenCompraRepositorio>();
builder.Services.AddScoped<IAutorizacionService, AutorizacionService>();

var key = builder.Configuration.GetValue<string>("JwtSettings:key");
var keyBytes = Encoding.ASCII.GetBytes(key);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});



builder.Services.AddCors(options => options.AddPolicy(name: "PsOrigins", policy => { policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader(); }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PsOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
