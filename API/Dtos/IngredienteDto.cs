namespace API.Dtos;
public class IngredienteDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public Decimal Precio { get; set; }
    public int Stock { get; set; }

    //las List<>
    //public List<HamburguesaDto> Hamburguesas { get; set; }
    //public List<Hamburguesa_ingredientes> Hamburguesa_Ingredientes { get; set; }
        
}
