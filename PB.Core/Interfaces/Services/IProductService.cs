using PB.Core.Dtos;
using System.Threading.Tasks;

namespace PB.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(long id);

        Task<ProductDto> SaveProductAsync(ProductDto product);
    }
}
