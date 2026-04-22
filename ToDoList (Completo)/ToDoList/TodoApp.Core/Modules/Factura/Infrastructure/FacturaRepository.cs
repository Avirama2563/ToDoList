using Microsoft.Data.Sqlite;
using TodoApp.Core.Modules.Facturas.Application.Interfaces;
using TodoApp.Core.Modules.Facturas.Domain;
using TodoApp.Core.Shared;

namespace TodoApp.Core.Modules.Facturas.Infrastructure;

public class FacturaRepository : IFacturaRepository
{
    private readonly Conexion _conexion;

    public FacturaRepository(Conexion conexion)
    {
        _conexion = conexion;
    }

    public async Task CrearAsync(Factura factura)
    {
        using var conn = _conexion.GetConnection();
        await conn.OpenAsync();

        using var transaction = conn.BeginTransaction();

        try
        {
            // 🔥 1. Insertar factura
            var cmdFactura = new SqliteCommand(
                @"INSERT INTO Facturas (ClienteId, Fecha, Total)
                  OUTPUT INSERTED.Id
                  VALUES (@ClienteId, @Fecha, @Total)",
                conn, transaction);

            cmdFactura.Parameters.AddWithValue("@ClienteId", factura.ClienteId);
            cmdFactura.Parameters.AddWithValue("@Fecha", factura.Fecha);
            cmdFactura.Parameters.AddWithValue("@Total", factura.Total);

            var facturaId = (int)await cmdFactura.ExecuteScalarAsync();

            // 🔥 2. Insertar detalles
            foreach (var detalle in factura.Detalles)
            {
                var cmdDetalle = new SqliteCommand(
                    @"INSERT INTO DetalleFacturas 
                      (FacturaId, ProductoId, Cantidad, PrecioUnitario)
                      VALUES (@FacturaId, @ProductoId, @Cantidad, @Precio)",
                    conn, transaction);

                cmdDetalle.Parameters.AddWithValue("@FacturaId", facturaId);
                cmdDetalle.Parameters.AddWithValue("@ProductoId", detalle.ProductoId);
                cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                cmdDetalle.Parameters.AddWithValue("@Precio", detalle.PrecioUnitario);

                await cmdDetalle.ExecuteNonQueryAsync();
            }

            // ✅ TODO OK
            transaction.Commit();
        }
        catch
        {
            // ❌ ERROR → revierte todo
            transaction.Rollback();
            throw;
        }
    }

    public async Task<List<Factura>> ObtenerTodasAsync()
    {
        var lista = new List<Factura>();

        using var conn = _conexion.GetConnection();
        await conn.OpenAsync();

        // 🔥 Obtener facturas
        var cmd = new SqliteCommand(
            "SELECT Id, ClienteId, Fecha, Total FROM Facturas",
            conn);

        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new Factura
            {
                Id = reader.GetInt32(0),
                ClienteId = reader.GetInt32(1),
                Fecha = reader.GetDateTime(2),
                Total = reader.GetDecimal(3),
                Detalles = new List<DetalleFactura>()
            });
        }

        reader.Close();

        // 🔥 Obtener detalles por factura
        foreach (var factura in lista)
        {
            var cmdDetalles = new SqliteCommand(
                @"SELECT Id, FacturaId, ProductoId, Cantidad, PrecioUnitario 
                  FROM DetalleFacturas 
                  WHERE FacturaId = @FacturaId",
                conn);

            cmdDetalles.Parameters.AddWithValue("@FacturaId", factura.Id);

            using var readerDetalles = await cmdDetalles.ExecuteReaderAsync();

            while (await readerDetalles.ReadAsync())
            {
                factura.Detalles.Add(new DetalleFactura
                {
                    Id = readerDetalles.GetInt32(0),
                    FacturaId = readerDetalles.GetInt32(1),
                    ProductoId = readerDetalles.GetInt32(2),
                    Cantidad = readerDetalles.GetInt32(3),
                    PrecioUnitario = readerDetalles.GetDecimal(4)
                });
            }
        }

        return lista;
    }
}