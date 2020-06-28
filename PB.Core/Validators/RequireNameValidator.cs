using PB.Core.Interfaces.Validators;
using System;
using System.Threading.Tasks;

namespace PB.Core.Validators
{
    public class RequireNameValidator : INameValidator
    {
        private const string ExceptionMessage = "Не указано название продукта";

        public async Task<bool> ValidateAsync(string name)
        {
            return !string.IsNullOrWhiteSpace(name) ? true : throw new Exception(ExceptionMessage);
        }
    }
}
