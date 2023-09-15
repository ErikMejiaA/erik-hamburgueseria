using System.Linq.Expressions;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
public class Hamburguesa_ingredientesRepository : IHamburguesa_ingredientesInterface
{
    private readonly DbAppContext _context;

    public Hamburguesa_ingredientesRepository(DbAppContext context)
    {
        _context = context;
    }

    public void Add(Hamburguesa_ingredientes entity)
    {
        _context.Set<Hamburguesa_ingredientes>().Add(entity);
    }

    public void AddRange(IEnumerable<Hamburguesa_ingredientes> entities)
    {
        _context.Set<Hamburguesa_ingredientes>().AddRange(entities);
    }

    public IEnumerable<Hamburguesa_ingredientes> Find(Expression<Func<Hamburguesa_ingredientes, bool>> expression)
    {
        return _context.Set<Hamburguesa_ingredientes>().Where(expression);
    }

    public async Task<IEnumerable<Hamburguesa_ingredientes>> GetAllAsync()
    {
        return await _context.Set<Hamburguesa_ingredientes>().ToListAsync();
    }

    public async Task<(int totalRegistros, IEnumerable<Hamburguesa_ingredientes> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var totalRegistros = await _context.Set<Hamburguesa_ingredientes>().CountAsync();
        var registros = await _context.Set<Hamburguesa_ingredientes>()
                                                        .Skip((pageIndex - 1) * pageSize)
                                                        .Take(pageSize)
                                                        .ToListAsync();

        return (totalRegistros, registros);
    }

    public async Task<Hamburguesa_ingredientes> GetByIdAsync(int idHamburguesa, int idIngrediente)
    {
        return await _context.Set<Hamburguesa_ingredientes>().FirstOrDefaultAsync(p => (p.Hamburguesa_id == idHamburguesa && p.Ingrediente_id == idIngrediente));
    }

    public void Remove(Hamburguesa_ingredientes entity)
    {
        _context.Set<Hamburguesa_ingredientes>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<Hamburguesa_ingredientes> entities)
    {
        _context.Set<Hamburguesa_ingredientes>().RemoveRange(entities);
    }

    public void Update(Hamburguesa_ingredientes entity)
    {
        _context.Set<Hamburguesa_ingredientes>().Update(entity);
    }
        
}

