using Scalar.AspNetCore;

// 🔌 Shared
using TodoApp.Core.Shared;

// 👤 Clientes
using TodoApp.Core.Modules.Clientes.Application.Interfaces;
using TodoApp.Core.Modules.Clientes.Application.UseCases;
using TodoApp.Core.Modules.Clientes.Infrastructure;

// 📦 Productos / Categorias
using TodoApp.Core.Modules.Productos.Application.Interfaces;
using TodoApp.Core.Modules.Productos.Application.UseCases;
using TodoApp.Core.Modules.Productos.Infrastructure;

// 🧾 Facturas
using TodoApp.Core.Modules.Facturas.Application.Interfaces;
using TodoApp.Core.Modules.Facturas.Application.UseCases;
using TodoApp.Core.Modules.Facturas.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


// 🔥 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Facturacion API", Version = "v1" });
});


// 🔥 🔌 CONEXIÓN DESDE appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 👇 PASAMOS EL STRING AL CORE (FORMA CORRECTA)
builder.Services.AddSingleton(new Conexion(connectionString));


// 🔹 REPOSITORIES

// Clientes
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

// Productos y Categorías
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

// Facturas
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();



// 🔹 USE CASES

// Clientes
builder.Services.AddScoped<CrearClienteUseCase>();
builder.Services.AddScoped<ObtenerClientesUseCase>();

// Productos
builder.Services.AddScoped<CrearProductoUseCase>();
builder.Services.AddScoped<ObtenerProductosUseCase>();

// Categorías
builder.Services.AddScoped<CrearCategoriaUseCase>();
builder.Services.AddScoped<ObtenerCategoriasUseCase>();

// Facturas
builder.Services.AddScoped<CrearFacturaUseCase>();
builder.Services.AddScoped<ObtenerFacturasUseCase>();



var app = builder.Build();


// 🔥 Swagger
app.UseSwagger();


// 🔥 Scalar (interfaz bonita)
app.MapScalarApiReference(options =>
{
    options.WithOpenApiRoutePattern("/swagger/{documentName}/swagger.json");
});


// 🔥 ENDPOINTS
app.MapClientesEndpoints();
app.MapProductosEndpoints();
app.MapCategoriasEndpoints();
app.MapFacturasEndpoints(); // quítalo si no lo tienes


app.Run();