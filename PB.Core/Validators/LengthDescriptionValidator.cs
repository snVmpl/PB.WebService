using PB.Core.Interfaces.Validators;
using System;
using System.Threading.Tasks;

namespace PB.Core.Validators
{
    public class LengthDescriptionValidator : IDescriptionValidator
    {
        private const string ExceptionMessage = "Описание продукта слишком длинное";

        public async Task<bool> ValidateAsync(string description)
        {
            var result = string.IsNullOrWhiteSpace(description) || description.Length < 501;

            return result ? true : throw new Exception(ExceptionMessage);
        }
    }
}
