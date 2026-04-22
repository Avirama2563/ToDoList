using TodoApp.Core.Modules.Clientes.Domain;

namespace TodoApp.Core.Modules.Clientes.Application.Interfaces;

public interface IClienteRepository
{
    Task CrearAsync(Cliente cliente);

    Task<List<Cliente>> ObtenerTodosAsync();

    Task<Cliente?> ObtenerPorIdAsync(int id);
}