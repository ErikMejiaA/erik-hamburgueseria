

using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IUsuario : IGenericInterface<Usuario>
    {
        Task<Usuario> GetByUsernameAsync(string username);
    }
}