namespace Dominio.Entities;
public class Ingrediente : BaseEntity
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public Decimal Precio { get; set; }
    public int Stock { get; set; }

    //las ICollection<>
    public ICollection<Hamburguesa> Hamburguesas { get; set; } = new HashSet<Hamburguesa>();

    public ICollection<Hamburguesa_ingredientes> Hamburguesa_Ingredientes { get; set; }
        
}
