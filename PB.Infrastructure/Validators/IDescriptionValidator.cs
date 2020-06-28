using System.Threading.Tasks;

namespace PB.Infrastructure.Validators
{
    public interface IDescriptionValidator : IValidator
    {
        Task<bool> ValidateAsync(string description);
    }
}
