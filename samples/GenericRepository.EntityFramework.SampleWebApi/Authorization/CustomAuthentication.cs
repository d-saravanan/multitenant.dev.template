using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin;
using System.Net;
using System.Security.Claims;
using System.Collections.Generic;
using Owin;

namespace MultiTenantRepositry.EF.Core.Authorization
{
    public class CustomAuthenticationMiddleware : OwinMiddleware
    {
        public CustomAuthenticationMiddleware(OwinMiddleware next) : base(next)
        {
        }

        const string UserId = "8fdb3abe-811e-4e20-98b9-4cbdb673e0be", tenantId = "8fdb3abe-811e-4e20-98b9-4cbdb673e2be";
        string[] roles = new[] { "Member", "Administrator" };

        public override Task Invoke(IOwinContext context)
        {
            var authHeader = context.Request.Headers.Get("Authorization");
            if (!string.IsNullOrWhiteSpace(authHeader))
            {
                var defaultClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid,UserId),
                    new Claim(ClaimTypes.GroupSid,tenantId),
                    new Claim(ClaimTypes.Upn,"John Smith"),
                    new Claim(ClaimTypes.Role,string.Join(",",roles))
                };

                var principal = new ClaimsPrincipal(new ClaimsIdentity(defaultClaims));

                Thread.CurrentPrincipal = principal;
                context.Authentication.User = principal;
            }
            return Next.Invoke(context);
        }
    }
}
