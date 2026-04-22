using Microsoft.Data.Sqlite;
using TodoApp.Core.Modules.Productos.Application.Interfaces;
using TodoApp.Core.Modules.Productos.Domain;
using TodoApp.Core.Shared;

namespace TodoApp.Core.Modules.Productos.Infrastructure;

public class ProductoRepository : IProductoRepository
{
    private readonly Conexion _conexion;

    public ProductoRepository(Conexion conexion)
    {
        _conexion = conexion;
    }

    public async Task CrearAsync(Producto producto)
    {
        using var conn = _conexion.GetConnection();
        await conn.OpenAsync();

        var cmd = new SqliteCommand(
            "INSERT INTO Productos (Nombre, Precio, CategoriaId) VALUES (@Nombre, @Precio, @CategoriaId)",
            conn);

        cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
        cmd.Parameters.AddWithValue("@Precio", producto.Precio);
        cmd.Parameters.AddWithValue("@CategoriaId", producto.CategoriaId);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<Producto>> ObtenerTodosAsync()
    {
        var lista = new List<Producto>();

        using var conn = _conexion.GetConnection();
        await conn.OpenAsync();

        var cmd = new SqliteCommand(
            "SELECT Id, Nombre, Precio, CategoriaId FROM Productos",
            conn);

        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new Producto
            {
                Id = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                Precio = reader.GetDecimal(2),
                CategoriaId = reader.GetInt32(3)
            });
        }

        return lista;
    }
}