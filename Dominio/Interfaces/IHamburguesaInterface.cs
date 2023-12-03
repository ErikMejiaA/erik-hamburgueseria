using Dominio.Entities;

namespace Dominio.Interfaces;
public interface IHamburguesaInterface : IGenericInterface<Hamburguesa>
{
    //nuevos metodos 
    Task<IEnumerable<Hamburguesa>> GetAllHamburIngredAsync(string ingrediente);
    Task<IEnumerable<Hamburguesa>> GetAllHamburguesaSinAsync(string ingrediente);
    Task<IEnumerable<Hamburguesa>> GetAllPrecioHamburguesaMenorAsync(decimal precio);
    Task<IEnumerable<Hamburguesa>> GetAllOrdenAscendenteAsync();
    Task<Hamburguesa> GetHamburguesaByNameAsync(string nombre);
        
}
