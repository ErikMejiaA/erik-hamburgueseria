using Dominio.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Dominio.Interfaces;
public interface IChefInterface : IGenericInterface<Chef>
{
    //nuevos metodos 
    Task<IEnumerable<Chef>> GetAllChefsCarnesAsync(string tipoCarne);
    Task<IEnumerable<Chef>> GetAllHamburguesasChefAsync(string nombre);
        
}
