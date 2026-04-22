using TodoApp.Core.Modules.Clientes.Application.UseCases;
using TodoApp.Core.Modules.Clientes.Domain;

public static class ClientesEndpoints
{
    public static void MapClientesEndpoints(this WebApplication app)
    {
        app.MapGet("/clientes", async (ObtenerClientesUseCase useCase) =>
        {
            return await useCase.EjecutarAsync();
        });

        app.MapPost("/clientes", async (CrearClienteUseCase useCase, Cliente cliente) =>
        {
            await useCase.EjecutarAsync(cliente);
            return Results.Ok("Cliente creado");
        });
    }
}