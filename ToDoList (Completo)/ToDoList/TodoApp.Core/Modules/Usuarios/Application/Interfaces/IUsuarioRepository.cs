using TodoApp.Core.Modules.Usuarios.Domain;

namespace TodoApp.Core.Modules.Usuarios.Application.Interfaces;

public interface IUsuarioRepository
{
    Task CrearAsync(Usuario usuario);
    Task<List<Usuario>> ObtenerTodosAsync();
    Task<Usuario?> ObtenerPorIdAsync(int id);
}