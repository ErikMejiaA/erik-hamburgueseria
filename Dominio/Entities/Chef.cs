namespace Dominio.Entities;
public class Chef : BaseEntity
{
    public string Nombre { get; set; }
    public string Especialidad { get; set; }

    //la ICollection<>
    public ICollection<Hamburguesa> Hamburguesas { get; set; }    
}
