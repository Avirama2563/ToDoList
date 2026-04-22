using Microsoft.Data.Sqlite;
using TodoApp.Core.Modules.Productos.Application.Interfaces;
using TodoApp.Core.Modules.Productos.Domain;
using TodoApp.Core.Shared;

namespace TodoApp.Core.Modules.Productos.Infrastructure;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly Conexion _conexion;

    public CategoriaRepository(Conexion conexion)
    {
        _conexion = conexion;
    }

    public async Task CrearAsync(Categoria categoria)
    {
        using var conn = _conexion.GetConnection();
        await conn.OpenAsync();

        var cmd = new SqliteCommand(
            "INSERT INTO Categorias (Nombre) VALUES (@Nombre)",
            conn);

        cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<Categoria>> ObtenerTodasAsync()
    {
        var lista = new List<Categoria>();

        using var conn = _conexion.GetConnection();
        await conn.OpenAsync();

        var cmd = new SqliteCommand(
            "SELECT Id, Nombre FROM Categorias",
            conn);

        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new Categoria
            {
                Id = reader.GetInt32(0),
                Nombre = reader.GetString(1)
            });
        }

        return lista;
    }
}