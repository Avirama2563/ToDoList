using TodoApp.Core.Modules.Usuarios.Application.Interfaces;
using TodoApp.Core.Modules.Usuarios.Domain;
using Microsoft.Data.Sqlite;
using TodoApp.Core.Shared;

namespace TodoApp.Core.Modules.Usuarios.Infrastructure;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly Conexion _conexion;

    public UsuarioRepository(Conexion conexion)
    {
        _conexion = conexion;
    }

    public async Task CrearAsync(Usuario usuario)
    {
        using var conn = _conexion.GetConnection();
        await conn.OpenAsync();

        var cmd = new SqliteCommand(
            @"INSERT INTO usuarios (nombre, rol) 
              VALUES (@nombre, @rol)", conn);

        cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
        cmd.Parameters.AddWithValue("@rol", usuario.Rol);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<Usuario>> ObtenerTodosAsync()
    {
        var lista = new List<Usuario>();

        using var conn = _conexion.GetConnection();
        await conn.OpenAsync();

        var cmd = new SqliteCommand(
            "SELECT id_usuario, nombre, rol FROM usuarios", conn);

        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new Usuario
            {
                Id = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                Rol = reader.GetString(2)
            });
        }

        return lista;
    }

    public async Task<Usuario?> ObtenerPorIdAsync(int id)
    {
        using var conn = _conexion.GetConnection();
        await conn.OpenAsync();

        var cmd = new SqliteCommand(
            @"SELECT id_usuario, nombre, rol 
              FROM usuarios 
              WHERE id_usuario = @id", conn);

        cmd.Parameters.AddWithValue("@id", id);

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new Usuario
            {
                Id = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                Rol = reader.GetString(2)
            };
        }

        return null;
    }
}