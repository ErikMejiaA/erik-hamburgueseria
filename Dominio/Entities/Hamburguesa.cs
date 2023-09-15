namespace Dominio.Entities;
public class Hamburguesa : BaseEntity
{
    public string Nombre { get; set; }
    public Decimal Precio { get; set; }

    //llaves foraneas 
    public int Categoria_id { get; set; }
    public int Chef_id { get; set; }

    //referencias
    public Chef Chef { get; set; }
    public Categoria Categoria { get; set; }

    //las ICollection<>
    public ICollection<Hamburguesa_ingredientes> Hamburguesa_Ingredientes { get; set; }
    public ICollection<Ingrediente> Ingredientes { get; set; } = new HashSet<Ingrediente>();
}
