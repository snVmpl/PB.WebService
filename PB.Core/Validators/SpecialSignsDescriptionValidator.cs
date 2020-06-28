using PB.Core.Interfaces.Validators;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PB.Core.Validators
{
    public class SpecialSignsDescriptionValidator : IDescriptionValidator
    {
        private const string ExceptionMessage = "Описание продукта содержит специальные символы";

        public async Task<bool> ValidateAsync(string description)
        {
            var result = string.IsNullOrWhiteSpace(description) ||
                         Regex.IsMatch(description, "^[а-яА-Яa-zA-Z0-9\\s\\'\\,\\.]*$");

            return result ? true : throw new Exception(ExceptionMessage);
        }
    }
}
