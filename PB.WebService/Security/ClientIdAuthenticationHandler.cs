using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace PB.WebService.Security
{
    /// <summary>
    /// Handler checks auth and parity of clientId
    /// </summary>
    public class ClientIdAuthenticationHandler : AuthenticationHandler<ClientIdAuthenticationOptions>
    {
        public ClientIdAuthenticationHandler(IOptionsMonitor<ClientIdAuthenticationOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string authHeader = Request.Headers[Options.AuthorizationHeaderKey];

            if (!string.IsNullOrWhiteSpace(authHeader) && authHeader.ToLower().StartsWith(Options.ClientIdParamName))
            {
                var clientId = authHeader.Split(' ')[1];

                if (string.IsNullOrWhiteSpace(clientId) || !long.TryParse(clientId, out var id))
                    return AuthenticateResult.Fail("Invalid authentication provided, access denied.");

                var user = new GenericPrincipal(new GenericIdentity(clientId), null);

                var claims = new List<Claim>
                {
                    new Claim("ClientId", clientId)
                };

                var appIdentity = new ClaimsIdentity(claims);

                user.AddIdentity(appIdentity);

                var ticket = new AuthenticationTicket(user, new AuthenticationProperties(), Options.AuthenticationScheme);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Invalid authentication provided, access denied.");
        }
    }
}
