using Dominio.Entities;

namespace Dominio.Interfaces;
public interface IIngredienteInterface : IGenericInterface<Ingrediente>
{
    //nuevos metodos
   Task<IEnumerable<Ingrediente>> GetAllIngredientesAsync(int stock);  
}
