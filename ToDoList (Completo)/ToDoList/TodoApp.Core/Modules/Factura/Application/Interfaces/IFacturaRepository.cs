using TodoApp.Core.Modules.Facturas.Domain;

namespace TodoApp.Core.Modules.Facturas.Application.Interfaces;

public interface IFacturaRepository
{
    Task CrearAsync(Factura factura);

    Task<List<Factura>> ObtenerTodasAsync();
}