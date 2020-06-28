using PB.Core.Interfaces.Validators;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PB.Core.Validators
{
    public class SpecialSignsNameValidator : INameValidator
    {
        private const string ExceptionMessage = "Название продукта содержит специальные символы";

        public async Task<bool> ValidateAsync(string name)
        {
            var result = Regex.IsMatch(name, "^[а-яА-Яa-zA-Z0-9\\s\\'\\,\\.]*$");

            return result ? true : throw new Exception(ExceptionMessage);
        }
    }
}
