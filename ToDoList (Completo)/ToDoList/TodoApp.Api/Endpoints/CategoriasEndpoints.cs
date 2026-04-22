using TodoApp.Core.Modules.Productos.Application.UseCases;
using TodoApp.Core.Modules.Productos.Domain;

public static class CategoriasEndpoints
{
    public static void MapCategoriasEndpoints(this WebApplication app)
    {
        app.MapGet("/categorias", async (ObtenerCategoriasUseCase useCase) =>
        {
            return await useCase.EjecutarAsync();
        });

        app.MapPost("/categorias", async (CrearCategoriaUseCase useCase, Categoria categoria) =>
        {
            await useCase.EjecutarAsync(categoria);
            return Results.Ok("Categoría creada");
        });
    }
}