using Microsoft.Data.Sqlite;

namespace TodoApp.Core.Shared;

public class Conexion
{
    private readonly string _connectionString;

    public Conexion(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SqliteConnection GetConnection()
    {
        return new SqliteConnection(_connectionString);
    }
}