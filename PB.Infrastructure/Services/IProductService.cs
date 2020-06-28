using PB.Infrastructure.Dtos;
using System.Threading.Tasks;

namespace PB.Infrastructure.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(long id);

        Task<ProductDto> SaveProductAsync(ProductDto product);
    }
}
