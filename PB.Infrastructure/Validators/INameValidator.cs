using System.Threading.Tasks;

namespace PB.Infrastructure.Validators
{
    public interface INameValidator : IValidator
    {
        Task<bool> ValidateAsync(string name);
    }
}
