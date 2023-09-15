using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
public class HamburguesaRepository : GenericRepository<Hamburguesa>, IHamburguesaInterface
{
    private readonly DbAppContext _context;

    public HamburguesaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    //nuevos metodos a implementar 
    public override async Task<Hamburguesa> GetByIdAsync(int id)
    {
        return await _context.Set<Hamburguesa>()
        .Include(p => p.Ingredientes)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<Hamburguesa>> GetAllAsync()
    {
        return await _context.Set<Hamburguesa>()
        .Include(p => p.Ingredientes)
        .ToListAsync();
        
    }

    public override async Task<(int totalRegistros, IEnumerable<Hamburguesa> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Hamburguesas as IQueryable<Hamburguesa>;

        if (!string.IsNullOrEmpty(search)) 
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }

        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Ingredientes)
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

        return (totalRegistros, registros);
    }


}
