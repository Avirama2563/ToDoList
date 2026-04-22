using TodoApp.Core.Modules.Productos.Application.Interfaces;
using TodoApp.Core.Modules.Productos.Domain;

namespace TodoApp.Core.Modules.Productos.Application.UseCases;

public class CrearProductoUseCase
{
    private readonly IProductoRepository _repo;

    public CrearProductoUseCase(IProductoRepository repo)
    {
        _repo = repo;
    }

    public async Task EjecutarAsync(Producto producto)
    {
        if (string.IsNullOrWhiteSpace(producto.Nombre))
            throw new Exception("El nombre es obligatorio");

        if (producto.Precio <= 0)
            throw new Exception("El precio debe ser mayor a 0");

        await _repo.CrearAsync(producto);
    }
}