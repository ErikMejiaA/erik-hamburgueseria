using Dominio.Entities;

namespace Dominio.Interfaces;
public interface ICategoriaInterface : IGenericInterface<Categoria>
{
    //nuevos metodos 
    Task<IEnumerable<Categoria>> GetAllHamburguesasAsync(string categoria);
        
}
