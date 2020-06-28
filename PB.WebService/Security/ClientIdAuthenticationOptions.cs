using Microsoft.AspNetCore.Authentication;

namespace PB.WebService.Security
{
    public class ClientIdAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string Scheme = "ClientId";

        public string AuthenticationScheme => Scheme;

        public string AuthorizationHeaderKey => "Authorization";
        public string ClientIdParamName => "clientid";
    }
}
