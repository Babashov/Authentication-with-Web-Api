using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;

namespace AuthTesty.Controllers
{
    public class AuthController : ApiController
    {
        public String Get()
        {
            IDictionary<string, object> payload = new Dictionary<string, object>
            {
                {"memberId",43},
                {"age",34 },
                {"name","mike" },
                {"role","admin" }
            };

            TokenManager manager = new TokenManager();
            
            return manager.IssueToken(payload);
        }

        [HttpPost]
        [Auth(UserRole.Admin)]
        [Authorize(Roles = "admin")]
        public string Post(string str)
        {
            return RequestContext.Principal.Identity.Name;
        }

    }
}
