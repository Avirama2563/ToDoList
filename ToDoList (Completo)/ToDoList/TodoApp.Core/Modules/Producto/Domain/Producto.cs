namespace TodoApp.Core.Modules.Productos.Domain;

public class Producto
{
    public int Id { get; set; }

    public required string Nombre { get; set; }

    public decimal Precio { get; set; }

    public int CategoriaId { get; set; }
}