using TodoApp.Core.Modules.Facturas.Application.UseCases;
using TodoApp.Core.Modules.Facturas.Domain;

public static class FacturasEndpoints
{
    public static void MapFacturasEndpoints(this WebApplication app)
    {
        app.MapGet("/facturas", async (ObtenerFacturasUseCase useCase) =>
        {
            return await useCase.EjecutarAsync();
        });

        app.MapPost("/facturas", async (CrearFacturaUseCase useCase, Factura factura) =>
        {
            await useCase.EjecutarAsync(factura);
            return Results.Ok("Factura creada correctamente");
        });
    }
}