using Microsoft.Data.Sqlite;
using TodoApp.Core.Modules.Clientes.Application.Interfaces;
using TodoApp.Core.Modules.Clientes.Domain;
using TodoApp.Core.Shared;

namespace TodoApp.Core.Modules.Clientes.Infrastructure;

public class ClienteRepository : IClienteRepository
{
    private readonly Conexion _conexion;

    public ClienteRepository(Conexion conexion)
    {
        _conexion = conexion;
    }

    public async Task CrearAsync(Cliente cliente)
    {
        using var conn = _conexion.GetConnection();
        await conn.OpenAsync();

        var cmd = new SqliteCommand(
            "INSERT INTO Clientes (Nombre, Documento) VALUES (@Nombre, @Documento)",
            conn);

        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
        cmd.Parameters.AddWithValue("@Documento", cliente.Documento);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<Cliente>> ObtenerTodosAsync()
    {
        var lista = new List<Cliente>();

        using var conn = _conexion.GetConnection();
        await conn.OpenAsync();

        var cmd = new SqliteCommand(
            "SELECT Id, Nombre, Documento FROM Clientes",
            conn);

        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new Cliente
            {
                Id = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                Documento = reader.GetString(2)
            });
        }

        return lista;
    }

    public async Task<Cliente?> ObtenerPorIdAsync(int id)
    {
        using var conn = _conexion.GetConnection();
        await conn.OpenAsync();

        var cmd = new SqliteCommand(
            "SELECT Id, Nombre, Documento FROM Clientes WHERE Id = @Id",
            conn);

        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new Cliente
            {
                Id = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                Documento = reader.GetString(2)
            };
        }

        return null;
    }
}