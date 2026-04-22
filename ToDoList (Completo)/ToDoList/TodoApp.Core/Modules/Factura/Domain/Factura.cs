namespace TodoApp.Core.Modules.Facturas.Domain;

public class Factura
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    public decimal Total { get; set; }

    public List<DetalleFactura> Detalles { get; set; } = new();
}