namespace Dominio.Entities;
public class Categoria : BaseEntity
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }

    //la ICollection<>
    public ICollection<Hamburguesa> Hamburguesas { get; set; }
        
}
