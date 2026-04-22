using TodoApp.Core.Modules.Productos.Application.Interfaces;
using TodoApp.Core.Modules.Productos.Domain;

namespace TodoApp.Core.Modules.Productos.Application.UseCases;

public class ObtenerProductosUseCase
{
    private readonly IProductoRepository _repo;

    public ObtenerProductosUseCase(IProductoRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Producto>> EjecutarAsync()
    {
        return await _repo.ObtenerTodosAsync();
    }
}