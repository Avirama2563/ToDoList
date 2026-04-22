using TodoApp.Core.Modules.Clientes.Application.Interfaces;
using TodoApp.Core.Modules.Clientes.Domain;

namespace TodoApp.Core.Modules.Clientes.Application.UseCases;

public class ObtenerClientesUseCase
{
    private readonly IClienteRepository _repo;

    public ObtenerClientesUseCase(IClienteRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Cliente>> EjecutarAsync()
    {
        return await _repo.ObtenerTodosAsync();
    }
}