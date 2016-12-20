using MultiTenancy.Core.ProviderContracts;
using System;
using System.Security.Claims;
using System.Threading;

namespace MultiTenancy.Core.Providers
{
    public class ClaimsContextDataProvider : IUserContextDataProvider
    {
        public Guid UserId
        {
            get
            {
                var userId = (Thread.CurrentPrincipal as ClaimsPrincipal)?.FindFirst(ClaimTypes.Sid)?.Value;
                return TryGetGuidFromString(userId);
            }
        }

        public string UserName
        {
            get
            {
                var userName = (Thread.CurrentPrincipal as ClaimsPrincipal)?.FindFirst(ClaimTypes.Upn)?.Value;
                return userName;
            }
        }

        public Guid TenantId
        {
            get
            {
                var tenantId = (Thread.CurrentPrincipal as ClaimsPrincipal)?.FindFirst(ClaimTypes.GroupSid)?.Value;
                return TryGetGuidFromString(tenantId);
            }
        }

        private static Guid TryGetGuidFromString(string arg)
        {
            Guid result = Guid.Empty;
            if (!string.IsNullOrEmpty(arg) && Guid.TryParse(arg, out result) && result != Guid.Empty)
            {
                return result;
            }
            return result;
            //throw new UnAuthenticatedException($"could not get the {nameof(arg)} from the claims");
        }
    }
}
