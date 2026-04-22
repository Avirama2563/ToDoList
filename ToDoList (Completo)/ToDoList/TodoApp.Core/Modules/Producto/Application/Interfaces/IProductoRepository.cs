using TodoApp.Core.Modules.Productos.Domain;

namespace TodoApp.Core.Modules.Productos.Application.Interfaces;

public interface IProductoRepository
{
    Task CrearAsync(Producto producto);

    Task<List<Producto>> ObtenerTodosAsync();
}