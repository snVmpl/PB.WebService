using System.Threading.Tasks;

namespace PB.Core.Interfaces.Validators
{
    public interface IDescriptionValidator : IValidator
    {
        Task<bool> ValidateAsync(string description);
    }
}
