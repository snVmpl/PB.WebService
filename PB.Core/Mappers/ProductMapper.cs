using PB.Core.Dtos;
using PB.Data.Entities;

namespace PB.Core.Mappers
{
    public static class ProductMapper
    {
        public static Product DtoToEntityMap(ProductDto dto)
        {
            var entity = new Product
            {
                Name = dto.Name,
                Description = dto.Description
            };

            return entity;
        }

        public static ProductDto EntityToDtoMap(Product entity)
        {
            var dto = new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };

            return dto;
        }
    }
}
