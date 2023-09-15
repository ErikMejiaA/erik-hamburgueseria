namespace API.Dtos;
public class ChefXhamburguesaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Especialidad { get; set; }

    //la List<>
    public List<HamburguesaDto> Hamburguesas { get; set; }
        
}
