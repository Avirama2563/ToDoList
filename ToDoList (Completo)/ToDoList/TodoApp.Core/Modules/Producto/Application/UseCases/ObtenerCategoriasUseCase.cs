using TodoApp.Core.Modules.Productos.Application.Interfaces;
using TodoApp.Core.Modules.Productos.Domain;

namespace TodoApp.Core.Modules.Productos.Application.UseCases;

public class ObtenerCategoriasUseCase
{
    private readonly ICategoriaRepository _repo;

    public ObtenerCategoriasUseCase(ICategoriaRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Categoria>> EjecutarAsync()
    {
        return await _repo.ObtenerTodasAsync();
    }
}