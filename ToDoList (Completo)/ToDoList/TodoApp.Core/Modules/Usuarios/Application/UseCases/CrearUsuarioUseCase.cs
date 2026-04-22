using TodoApp.Core.Modules.Usuarios.Application.Interfaces;
using TodoApp.Core.Modules.Usuarios.Domain;

namespace TodoApp.Core.Modules.Usuarios.Application.UseCases;

public class CrearUsuarioUseCase
{
    private readonly IUsuarioRepository _repo;

    public CrearUsuarioUseCase(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    public async Task EjecutarAsync(Usuario usuario)
    {
        await _repo.CrearAsync(usuario);
    }
}