using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
public class ChefRepository : GenericRepository<Chef>, IChefInterface
{
    private readonly DbAppContext _context;

    public ChefRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    //nuevos metodos de busqueda 

    public override async Task<Chef> GetByIdAsync(int id)
    {
        return await _context.Set<Chef>()
        .Include(p => p.Hamburguesas)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<Chef>> GetAllAsync()
    {
        return await _context.Set<Chef>()
        .Include(p => p.Hamburguesas)
        .ToListAsync();
        
    }

    public override async Task<(int totalRegistros, IEnumerable<Chef> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Chefs as IQueryable<Chef>;

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

    public async Task<IEnumerable<Chef>> GetAllChefsCarnesAsync(string tipoCarne)
    {
        var lstChefsCarne = _context.Set<Chef>()
        .Where(p => p.Especialidad.ToLower().Contains(tipoCarne.ToLower()))
        .ToListAsync();

        return await lstChefsCarne;
    }

    public async Task<IEnumerable<Chef>> GetAllHamburguesasChefAsync(string nombre)
    {
        var lstChefHamburguesa = _context.Set<Chef>()
        .Include(p => p.Hamburguesas)
        .Where(p => p.Nombre.ToLower().Contains(nombre.ToLower()))
        .ToListAsync();

        return await lstChefHamburguesa;
    }
}
