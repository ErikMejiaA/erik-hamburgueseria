using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaInterface
{
    private readonly DbAppContext _context;

    public CategoriaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    //implemtacion de nuevos metodos sobrevarga de metodos 
    public override async Task<Categoria> GetByIdAsync(int id)
    {
        return await _context.Set<Categoria>()
        .Include(p => p.Hamburguesas)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        return await _context.Set<Categoria>()
        .Include(p => p.Hamburguesas)
        .ToListAsync();
        
    }

    public override async Task<(int totalRegistros, IEnumerable<Categoria> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Categorias as IQueryable<Categoria>;

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

    
    
}