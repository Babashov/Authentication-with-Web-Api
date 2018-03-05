using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Web.Http.Results;
using System.Net.Http.Headers;

namespace AuthTesty
{
    public class AuthAttribute : Attribute, IAuthenticationFilter
    {
        public AuthAttribute(UserRole allowedRole)
        {
            allowedRole = AllowedRoles;
        }
        public bool AllowMultiple => false;
        public UserRole AllowedRoles{ get; set; }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var token = context.Request.Headers.Authorization;

            if(token != null)
            {
                TokenManager manager = new TokenManager();
                try
                {
                    IDictionary<string, string> decoded = manager.DecodedToken(token.ToString());
                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name,decoded["name"]),
                    new Claim(ClaimTypes.Email,decoded["email"]),
                    new Claim(ClaimTypes.Role,decoded["role"])
                    };
                    context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims,"AuthenticationType"));
                    var role = (UserRole)Enum.Parse(typeof(UserRole), decoded["role"], true);
                    if((role & AllowedRoles) != 0)
                    {
                        return Task.FromResult(0);
                    }
                    
                }
                catch
                {
                   
                }
               
            }
            context.ErrorResult = new UnauthorizedResult(Enumerable.Empty<AuthenticationHeaderValue>(), context.Request);
            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}