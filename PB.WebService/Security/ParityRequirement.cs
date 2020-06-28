using Microsoft.AspNetCore.Authorization;

namespace PB.WebService.Security
{
    public class ParityRequirement : IAuthorizationRequirement
    {
        private bool IsEven { get; }

        public string ClaimKey => "ClientId";

        public ParityRequirement(bool isEven)
        {
            IsEven = isEven;
        }

        public bool ValidateParity(string clientIdClaim)
        {
            var parseResult = long.TryParse(clientIdClaim, out var client);

            if (!parseResult)
                return false;

            return IsEven ? client % 2 == 0 : client % 2 == 1;
        }
    }
}
