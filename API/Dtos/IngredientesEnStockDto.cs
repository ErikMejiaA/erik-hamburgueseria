
namespace API.Dtos;
public class IngredientesEnStockDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public Decimal Precio { get; set; }
    public int Stock { get; set; }
        
}
