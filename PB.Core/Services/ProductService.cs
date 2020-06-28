using PB.Core.Dtos;
using PB.Core.Interfaces.Services;
using PB.Core.Mappers;
using PB.Data.Entities;
using PB.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PB.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IValidationService<Product> _productValidationService;

        public ProductService(IRepository<Product> productRepository, IValidationService<Product> productValidationService)
        {
            _productRepository = productRepository;
            _productValidationService = productValidationService;
        }

        public async Task<ProductDto> GetProductByIdAsync(long id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            var result = ProductMapper.EntityToDtoMap(product);
            return result;
        }

        public async Task<ProductDto> SaveProductAsync(ProductDto product)
        {
            var entity = ProductMapper.DtoToEntityMap(product);

            if (!await _productValidationService.Validate(entity))
                throw new ValidationException("The product didn't pass validation");

            entity = await _productRepository.InsertAsync(entity);

            return ProductMapper.EntityToDtoMap(entity);
        }
    }
}
