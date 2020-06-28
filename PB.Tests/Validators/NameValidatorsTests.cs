using PB.Core.Interfaces.Validators;
using PB.Core.Validators;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PB.Tests.Validators
{
    public class NameValidatorsTests
    {
        private readonly INameValidator _nameValidator;
        private readonly INameValidator _specialSignsNameValidator;

        public NameValidatorsTests()
        {
            _nameValidator = new RequireNameValidator();
            _specialSignsNameValidator = new SpecialSignsNameValidator();
        }

        [Fact]
        public async Task RequiredNameValidatorTest_CorrectName()
        {
            var result = await _nameValidator.ValidateAsync("Correct Name");

            Assert.True(result);
        }

        [Fact]
        public async Task RequiredNameValidator_EmptyName()
        {
            await Assert.ThrowsAnyAsync<Exception>(() => _nameValidator.ValidateAsync(string.Empty));
        }

        [Theory]
        [InlineData("")]
        [InlineData("This name doesn't contains special signs")]
        [InlineData("Страховой коробочный продукт")]
        public async Task SpecialSignsNameValidator_True(string name)
        {
            var result = await _specialSignsNameValidator.ValidateAsync(name);

            Assert.True(result);
        }

        [Theory]
        [InlineData("Oh, This name contains $peci@l signs")]
        public async Task SpecialSignsNameValidator_False(string name)
        {
            await Assert.ThrowsAnyAsync<Exception>(() => _specialSignsNameValidator.ValidateAsync(name));
        }
    }
}
