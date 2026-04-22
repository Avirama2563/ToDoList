using TodoApp.Core.Modules.Productos.Application.UseCases;
using TodoApp.Core.Modules.Productos.Domain;

public static class ProductosEndpoints
{
    public static void MapProductosEndpoints(this WebApplication app)
    {
        app.MapGet("/productos", async (ObtenerProductosUseCase useCase) =>
        {
            return await useCase.EjecutarAsync();
        });

        app.MapPost("/productos", async (CrearProductoUseCase useCase, Producto producto) =>
        {
            await useCase.EjecutarAsync(producto);
            return Results.Ok("Producto creado");
        });
    }
}