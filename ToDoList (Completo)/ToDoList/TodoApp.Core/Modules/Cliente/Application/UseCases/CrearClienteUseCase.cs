using TodoApp.Core.Modules.Clientes.Application.Interfaces;
using TodoApp.Core.Modules.Clientes.Domain;

namespace TodoApp.Core.Modules.Clientes.Application.UseCases;

public class CrearClienteUseCase
{
    private readonly IClienteRepository _repo;

    public CrearClienteUseCase(IClienteRepository repo)
    {
        _repo = repo;
    }

    public async Task EjecutarAsync(Cliente cliente)
    {
        // 🔥 Validación básica (importante)
        if (string.IsNullOrWhiteSpace(cliente.Nombre))
            throw new Exception("El nombre es obligatorio");

        if (string.IsNullOrWhiteSpace(cliente.Documento))
            throw new Exception("El documento es obligatorio");

        await _repo.CrearAsync(cliente);
    }
}