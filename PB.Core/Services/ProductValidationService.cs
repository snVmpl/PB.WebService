using Microsoft.Extensions.DependencyInjection;
using PB.Core.Interfaces.Services;
using PB.Core.Interfaces.Validators;
using PB.Data.Entities;
using System;
using System.Threading.Tasks;

namespace PB.Core.Services
{
    public class ProductValidationService : IValidationService<Product>
    {
        private readonly IServiceProvider _serviceProvider;

        public ProductValidationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<bool> Validate(Product entity)
        {
            var nameValidators = _serviceProvider.GetServices<INameValidator>();
            var descriptionValidators = _serviceProvider.GetServices<IDescriptionValidator>();

            foreach (var validator in nameValidators)
            {
                if (!await validator.ValidateAsync(entity.Name))
                    return false;
            }

            foreach (var validator in descriptionValidators)
            {
                if (!await validator.ValidateAsync(entity.Description))
                    return false;
            }

            return true;
        }
    }
}
