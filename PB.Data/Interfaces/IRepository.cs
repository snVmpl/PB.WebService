using System.Linq;
using System.Threading.Tasks;

namespace PB.Data.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(long id);

        Task<T> InsertAsync(T entity);
    }
}
