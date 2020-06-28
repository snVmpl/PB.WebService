using PB.Infrastructure.Models;
using System.Threading.Tasks;

namespace PB.Infrastructure.Services
{
    public interface IValidationService<in T> where T : class, IEntity
    {
        Task<bool> Validate(T entity);
    }
}
