using System.Linq.Expressions;
using Dominio.Entities;

namespace Dominio.Interfaces;
public interface IHamburguesa_ingredientesInterface
{
    Task<Hamburguesa_ingredientes> GetByIdAsync(int idHamburguesa, int idIngrediente);
    Task<IEnumerable<Hamburguesa_ingredientes>> GetAllAsync();
    IEnumerable<Hamburguesa_ingredientes> Find(Expression<Func<Hamburguesa_ingredientes, bool>> expression);
    Task<(int totalRegistros, IEnumerable<Hamburguesa_ingredientes> registros)> GetAllAsync(int pageIndex, int pageSize, string search);
    void Add(Hamburguesa_ingredientes entity);
    void AddRange(IEnumerable<Hamburguesa_ingredientes> entities);
    void Remove(Hamburguesa_ingredientes entity);
    void RemoveRange(IEnumerable<Hamburguesa_ingredientes> entities);
    void Update(Hamburguesa_ingredientes entity);

    //nuevos metodos 
}