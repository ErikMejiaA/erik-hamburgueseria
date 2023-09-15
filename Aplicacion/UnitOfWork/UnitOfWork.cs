

using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork;

public class UnitOfWork : IUnitOfWorkInterface, IDisposable
{
    private readonly DbAppContext _context;
    private RolRepository _rol;
    private UsuarioRepository _usuario;

    private CategoriaRepository _categorias;
    private ChefRepository _chefs;
    private Hamburguesa_ingredientesRepository _hamburguesa_Ingredientes;
    private HamburguesaRepository _hamburguesas;
    private IngredienteRepository _ingredientes;


    public UnitOfWork(DbAppContext context)
    {
        _context = context;
    }
    public IUsuario Usuarios
    {
        get
        {
            if (_usuario is not null)
            {
                return _usuario;
            }
            return _usuario = new UsuarioRepository(_context);
        }
    }
    public IRol Roles
    {
        get
        {
            if (_rol is not null)
            {
                return _rol;
            }
            return _rol = new RolRepository(_context);
        }
    }

    public ICategoriaInterface Categorias
    {
        get
        {
            if (_categorias == null) {
                _categorias = new CategoriaRepository(_context);
            }
            return _categorias;
        }
    }

    public IChefInterface Chefs
    {
        get
        {
            if (_chefs == null) {
                _chefs = new ChefRepository(_context);
            }
            return _chefs;
        }
    }

    public IHamburguesa_ingredientesInterface Hamburguesa_Ingredientes
    {
        get
        {
            if (_hamburguesa_Ingredientes == null) {
                _hamburguesa_Ingredientes = new Hamburguesa_ingredientesRepository(_context);
            }
            return _hamburguesa_Ingredientes;
        }
    }

    public IHamburguesaInterface Hamburguesas 
    {
        get
        {
            if (_hamburguesas == null) {
                _hamburguesas = new HamburguesaRepository(_context);
            }
            return _hamburguesas;
        }
    }

    public IIngredienteInterface Ingredientes 
    {
        get
        {
            if (_ingredientes == null) {
                _ingredientes = new IngredienteRepository(_context);
            }
            return _ingredientes;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

}