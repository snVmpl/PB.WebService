using System.Threading.Tasks;

namespace PB.Core.Interfaces.Validators
{
    public interface INameValidator : IValidator
    {
        Task<bool> ValidateAsync(string name);
    }
}
