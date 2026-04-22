using TodoApp.Core.Modules.Facturas.Application.Interfaces;
using TodoApp.Core.Modules.Facturas.Domain;

namespace TodoApp.Core.Modules.Facturas.Application.UseCases;

public class ObtenerFacturasUseCase
{
    private readonly IFacturaRepository _repo;

    public ObtenerFacturasUseCase(IFacturaRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Factura>> EjecutarAsync()
    {
        return await _repo.ObtenerTodasAsync();
    }
}