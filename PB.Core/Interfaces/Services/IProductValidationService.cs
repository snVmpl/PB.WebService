using PB.Data.Interfaces;
using System.Threading.Tasks;

namespace PB.Core.Interfaces.Services
{
    public interface IValidationService<in T> where T : class, IEntity
    {
        Task<bool> Validate(T entity);
    }
}
