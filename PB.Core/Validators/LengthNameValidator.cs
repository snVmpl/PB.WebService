using PB.Core.Interfaces.Validators;
using System;
using System.Threading.Tasks;

namespace PB.Core.Validators
{
    public class LengthNameValidator : INameValidator
    {
        private const string ExceptionMessage = "Название продукта слишком длинное";

        public async Task<bool> ValidateAsync(string name)
        {
            var result = !string.IsNullOrWhiteSpace(name) && name.Length < 201;

            return result ? true : throw new Exception(ExceptionMessage);
        }
    }
}
