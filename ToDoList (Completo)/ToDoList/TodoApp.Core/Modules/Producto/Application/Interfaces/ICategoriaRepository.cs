using TodoApp.Core.Modules.Productos.Domain;

namespace TodoApp.Core.Modules.Productos.Application.Interfaces;

public interface ICategoriaRepository
{
    Task CrearAsync(Categoria categoria);

    Task<List<Categoria>> ObtenerTodasAsync();
}