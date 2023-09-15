
namespace API.Dtos;
public class CategoriaXhamburguesaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }

    //la List<>
    public List<HamburguesaDto> Hamburguesas { get; set; }
        
}
