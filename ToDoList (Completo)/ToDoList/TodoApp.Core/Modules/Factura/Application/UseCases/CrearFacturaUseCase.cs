using TodoApp.Core.Modules.Facturas.Application.Interfaces;
using TodoApp.Core.Modules.Facturas.Domain;

namespace TodoApp.Core.Modules.Facturas.Application.UseCases;

public class CrearFacturaUseCase
{
    private readonly IFacturaRepository _repo;

    public CrearFacturaUseCase(IFacturaRepository repo)
    {
        _repo = repo;
    }

    public async Task EjecutarAsync(Factura factura)
    {
        if (factura.ClienteId <= 0)
            throw new Exception("Cliente inválido");

        if (factura.Detalles == null || !factura.Detalles.Any())
            throw new Exception("La factura debe tener al menos un producto");

        // 🔥 Calcular total automáticamente
        factura.Total = factura.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);

        await _repo.CrearAsync(factura);
    }
}