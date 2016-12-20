using MultiTenancy.Core.Providers;
using System.Security.Claims;

namespace MultiTenancy.Core
{
    public static class IdentityExtensions
    {
        public static string Get<T>(this ClaimsPrincipal principal, string claimName)
        {
            if (!principal.Identity.IsAuthenticated) return null;

            var claim = principal.FindFirst(claimName);

            if (claim == null) return null;

            return claim.Value;
        }
    }
}
