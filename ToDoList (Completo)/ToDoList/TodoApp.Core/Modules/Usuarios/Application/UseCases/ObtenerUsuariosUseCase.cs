using TodoApp.Core.Modules.Usuarios.Application.Interfaces;
using TodoApp.Core.Modules.Usuarios.Domain;

namespace TodoApp.Core.Modules.Usuarios.Application.UseCases;

public class ObtenerUsuariosUseCase
{
    private readonly IUsuarioRepository _repo;

    public ObtenerUsuariosUseCase(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Usuario>> EjecutarAsync()
    {
        return await _repo.ObtenerTodosAsync();
    }
}