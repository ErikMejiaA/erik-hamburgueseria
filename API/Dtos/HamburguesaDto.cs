namespace API.Dtos;
public class HamburguesaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public Decimal Precio { get; set; }

    //llaves foraneas 
    //public int Categoria_id { get; set; }
    //public int Chef_id { get; set; }

    //las List<>
    //public List<Hamburguesa_ingredientesDto> Hamburguesa_Ingredientes { get; set; }
    //public List<IngredienteDto> Ingredientes { get; set; } 
    
}
