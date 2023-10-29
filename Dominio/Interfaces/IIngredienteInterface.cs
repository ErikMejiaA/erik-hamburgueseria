using Dominio.Entities;

namespace Dominio.Interfaces;
public interface IIngredienteInterface : IGenericInterface<Ingrediente>
{
    //nuevos metodos
   Task<IEnumerable<Ingrediente>> GetAllIngredientesAsync(int stock); 
   Task<IEnumerable<Ingrediente>> GetAllIngrediXhamburAsync(string ingrediente); 
   Task<Ingrediente> GetIndredienteMasCaroAsync();
   Task<IEnumerable<Ingrediente>> GetAllPrecioRangoAsync(decimal limInferior, decimal limSuperior);
   Task<Ingrediente> GetEditarDescripcionAsync(string nombre);
}
