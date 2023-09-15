using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
public class IngredienteRepository : GenericRepository<Ingrediente>, IIngredienteInterface
{
    private readonly DbAppContext _context;

    public IngredienteRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
    // nuevos metodos 
    public override async Task<Ingrediente> GetByIdAsync(int id)
    {
        return await _context.Set<Ingrediente>()
        .Include(p => p.Hamburguesas)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<Ingrediente>> GetAllAsync()
    {
        return await _context.Set<Ingrediente>()
        .Include(p => p.Hamburguesas)
        .ToListAsync();
        
    }

    public override async Task<(int totalRegistros, IEnumerable<Ingrediente> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Ingredientes as IQueryable<Ingrediente>;

        if (!string.IsNullOrEmpty(search)) 
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }

        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Hamburguesas)
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

        return (totalRegistros, registros);
    }

    public async Task<IEnumerable<Ingrediente>> GetAllIngredientesAsync(int stock)
    {
        //return await _context.Set<Ingrediente>()
        //.Include(p => p.Hamburguesas)
        //.ToListAsync();
        var lstIngredientes = _context.Set<Ingrediente>()
        .Where(p => p.Stock < stock)
        .ToListAsync();

        return await lstIngredientes;
    }
}
