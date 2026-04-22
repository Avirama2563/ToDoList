using TodoApp.Core.Modules.Productos.Application.Interfaces;
using TodoApp.Core.Modules.Productos.Domain;

namespace TodoApp.Core.Modules.Productos.Application.UseCases;

public class CrearCategoriaUseCase
{
    private readonly ICategoriaRepository _repo;

    public CrearCategoriaUseCase(ICategoriaRepository repo)
    {
        _repo = repo;
    }

    public async Task EjecutarAsync(Categoria categoria)
    {
        if (string.IsNullOrWhiteSpace(categoria.Nombre))
            throw new Exception("El nombre es obligatorio");

        await _repo.CrearAsync(categoria);
    }
}