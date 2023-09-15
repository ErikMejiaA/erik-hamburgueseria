

namespace Dominio.Interfaces;

    public interface IUnitOfWorkInterface
    {
        IUsuario Usuarios {get;}
        IRol Roles {get;}
        
        ICategoriaInterface Categorias { get; }
        IChefInterface Chefs { get; }
        IHamburguesa_ingredientesInterface Hamburguesa_Ingredientes { get; }
        IHamburguesaInterface Hamburguesas { get; } 
        IIngredienteInterface Ingredientes { get; }

        Task<int> SaveAsync();
    }

