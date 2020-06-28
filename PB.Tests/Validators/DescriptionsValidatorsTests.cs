using PB.Core.Interfaces.Validators;
using PB.Core.Validators;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PB.Tests.Validators
{
    public class DescriptionsValidatorsTests
    {
        private readonly IDescriptionValidator _specialSignsDescriptionValidator;

        public DescriptionsValidatorsTests()
        {
            _specialSignsDescriptionValidator = new SpecialSignsDescriptionValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData("This description doesn't contain special signs")]
        [InlineData("Продукт для продаж через клиентские центры")]
        public async Task SpecialSignsDescriptionValidatorTest_True(string description)
        {
            var result = await _specialSignsDescriptionValidator.ValidateAsync(description);

            Assert.True(result);
        }

        [Theory]
        [InlineData("Oh, it's a bad description /// *")]
        public async Task SpecialSignsDescriptionValidatorTest_False(string description)
        {
            await Assert.ThrowsAnyAsync<Exception>(() => _specialSignsDescriptionValidator.ValidateAsync(description));
        }
    }
}
